declare global {
    interface Window {
        api: {
            kaoList: IKaolistApi
        }
    }
}

export interface IChartSnippetKaraokeUserItem {
    id: string;
    nickName: string;
}

export interface IChartSnippetKaraokeItem {
    no: string;
}

export interface IApiGlobalOption { }

export interface IKaolistChartsListApiOption extends IApiGlobalOption {
    part?: Array<'snippet'>;
    type?: 'discovered' | 'liked';
    startDate?: Date;
    endDate?: Date;
    maxResults?: number;
}

export interface IChartSnippet {
    title: string;
    created: Date | null;
    composer: IChartSnippetKaraokeUserItem;
    singgers: IChartSnippetKaraokeUserItem[];
    soundId?: string;
    karaoke?: { [key: string]: IChartSnippetKaraokeItem }
}

export interface IChartItem {
    id: string;
    etag: string;
    snippet?: IChartSnippet;
}

export interface IChartResponse {
    etag: string;
    items: IChartItem[];
}

type QueryType<T extends object = { [key: string]: string | number | string[] | number[] }> = {
    [P in keyof T]?: T[P] extends (string | number | string[] | number[] | undefined) ? T[P] : string | number | string[] | number[];
};

interface IKaolistChartsApi {
    list: (option?: IKaolistChartsListApiOption) => Promise<IChartResponse>;
}

interface IKaolistApi {
    charts: IKaolistChartsApi;
}

interface IKaolistApiConstructorProps {
    baseUrl: string;
}

const kaoListApiEndPoint = {
    chartsList: '/api/charts/list'
}

class KaoListApi implements IKaolistApi {
    private _baseUrl: string;

    private _charts: IKaolistChartsApi;

    constructor(props?: IKaolistApiConstructorProps) {
        this._baseUrl = props?.baseUrl ?? '';

        this._charts = {
            list: (option?: IKaolistChartsListApiOption) => {
                const queryBuild = (options?: IKaolistChartsListApiOption): QueryType<IKaolistChartsListApiOption> | undefined => {
                    if (options === undefined || Object.keys(options).length === 0) {
                        return;
                    }

                    const { startDate, endDate, ...rest } = options;
                    let q: QueryType<IKaolistChartsListApiOption> = { ...rest };

                    if (startDate) {
                        q.startDate = startDate.toISOString();
                    }

                    if (endDate) {
                        q.endDate = endDate.toISOString();
                    }

                    return q;
                }

                return this.getAsync(kaoListApiEndPoint.chartsList, queryBuild(option)).then(item => item.json() as Promise<IChartResponse>);
            }
        }
    }

    protected static buildQuery(query: QueryType) {
        let q = "";
        Object.keys(query).forEach(key => {
            const v = query[key];
            if (v === undefined) {
                return;
            }

            if (v instanceof Array) {
                for (const el of v) {
                    q += `${key}=${el}&`
                }
            } else {
                q += `${key}=${v}&`
            }
        });
        return q.slice(0, -1);
    }

    protected buildUrl = (path: string, query?: QueryType): string => {
        let url = this._baseUrl + path;
        if (query !== undefined && Object.values(query).filter(query => query !== undefined).length > 0) {
            url += `?${KaoListApi.buildQuery(query)}`;
        }

        return url;
    }

    getAsync = (path: string, query?: QueryType) => {
        return fetch(this.buildUrl(path, query));
    }

    get charts(): IKaolistChartsApi {
        return this._charts;
    }

}

const api = new KaoListApi();
window.api = {
    ...window.api,
    kaoList: api
}

export default api;
