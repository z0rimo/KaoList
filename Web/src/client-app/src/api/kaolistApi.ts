import authorizeService from "../api-authorization/AuthorizeService";
import { SongRating } from "../enums/SongRating";

declare global {
    interface Window {
        api: {
            kaoList: IKaolistApi
        }
    }
}

export interface IKaoListResponse {
    kind: string;
    etag?: string;
}

export interface IPageInfo {
    totalResults: number;
    resultPerPage: number;
}

export interface IKaoListPageResponse extends IKaoListResponse {
    nextPageToken?: number;
    prevPageToken?: number;
    pageInfo?: IPageInfo;
}

export interface IThumbnailResource {
    url?: string;
    width?: number;
    height?: number;
}

export interface II18nLanguageSnippet {
    name?: string;
}

export interface II18nLanguageResource extends IKaoListResponse {
    kind: string;
    id?: string;
    snippet?: II18nLanguageSnippet;
}

export interface ISongKaraoke {
    providerName?: string;
    no?: string;
}

export interface ISongStatistics {
    followCount?: number;
    blindCount?: number;
}

export interface ISongUser {
    id: string;
    nickname: string;
}

export interface ISongSnippet {
    created?: Date;
    title?: string;
    songUsers?: ISongUser[];
    composer?: string;
    thumbnail?: IThumbnailResource;
    i18nName?: II18nLanguageResource;
    karaoke?: ISongKaraoke;
}

export interface ISongResource extends IKaoListResponse {
    kind: string;
    id?: string;
    rating?: SongRating;
    snippet?: ISongSnippet;
    statistics?: ISongStatistics;
}

export interface ISongListResponse extends IKaoListPageResponse {
    kind: string;
    items?: ISongResource[];
}

export interface IDiscoverChartResource extends IKaoListResponse {
    kind: string;
    id?: string;
    snippet?: ISongSnippet;
}

export interface IDiscoverChartListResponse extends IKaoListPageResponse {
    kind: string;
    resources?: IDiscoverChartResource[];
}

export interface ILikedChartResource extends IKaoListResponse {
    kind: string;
    id?: string;
    snippet?: ISongSnippet;
}

export interface ILikedChartListResponse extends IKaoListPageResponse {
    kind: string;
    resources?: ILikedChartResource[];
}

export interface ISearchSongId {
    kind: string;
    id?: string;
}

export interface ISearchSnippet extends ISongSnippet {

}

export interface ISearchResource extends IKaoListResponse {
    kind: string;
    id?: ISearchSongId;
    snippet?: ISearchSnippet[];
}

export interface ISearchListResponse extends IKaoListPageResponse {
    kind: string;
    items?: ISearchResource[];
}

export interface IChartSnippetKaraokeUserItem {
    id: string;
    nickName: string;
}

export interface IChartSnippetKaraokeItem {
    no: string;
}

export interface ISongDetailResponse extends IKaoListPageResponse {
    kind: string;
    item?: ISongResource;
    otherSongs?: ISongResource[];
    otherMySongs?: ISongResource[];
}

export interface ISongRateResponse {
    statusCode: number;
}

export interface ISongGetRatingResource {
    id?: string;
    rating?: SongRating;
}

export interface ISongGetRatingResponse {
    resources: ISongGetRatingResource[];
}

export interface IApiGlobalOption {
    part?: Array<'snippet'>;
    offset?: number;
    maxResults?: number;
}

export interface IKaolistChartsListApiOption extends IApiGlobalOption {
    startDate?: Date;
    endDate?: Date;
    date?: string;
}

export interface IKaolistSearchListApiOption extends IApiGlobalOption {
    q?: string[];
}

export interface IKaolistSongDetailApiOption extends IApiGlobalOption {
    id?: string;
}

export interface IKaolistSongRateApiOption extends IApiGlobalOption {
    songId: string;
    rate: number;
}

export interface IKaolistSongGetRatingApiOption extends IApiGlobalOption {
    ids?: string[];
}

type QueryType<T extends object = { [key: string]: string | number | string[] | number[] }> = {
    [P in keyof T]?: T[P] extends (string | number | string[] | number[] | undefined) ? T[P] : string | number | string[] | number[];
};

interface IKaolistChartsApi {
    discoverChartList: (option?: IKaolistChartsListApiOption) => Promise<IDiscoverChartListResponse>;
    likedChartList: (option?: IKaolistChartsListApiOption) => Promise<ILikedChartListResponse>;
}

interface IKaolistSearchsApi {
    songSearchList: (option?: IKaolistSearchListApiOption) => Promise<ISearchListResponse>;
}

interface IKaolistSongsApi {
    songDetail: (option?: IKaolistSongDetailApiOption) => Promise<ISongDetailResponse>;
    songRate: (option?: IKaolistSongRateApiOption) => Promise<ISongRateResponse>;
    songGetRating: (option?: IKaolistSongGetRatingApiOption) => Promise<ISongGetRatingResponse>;
}

interface IKaolistApi {
    charts: IKaolistChartsApi;
    searchs: IKaolistSearchsApi;
    songs: IKaolistSongsApi;
}

interface IKaolistApiConstructorProps {
    baseUrl: string;
}

const kaoListApiEndPoint = {
    chartDiscover: '/api/charts/list/discover',
    chartLiked: 'api/charts/list/liked',
    searchSong: '/api/search/list',
    songDetail: '/api/songs/detail',
    songRate: 'api/songs/rate',
    songGetRating: 'api/songs/getRating',
}

class KaoListApi implements IKaolistApi {
    private _baseUrl: string;

    private _charts: IKaolistChartsApi;
    private _searchs: IKaolistSearchsApi;
    private _songs: IKaolistSongsApi;

    constructor(props?: IKaolistApiConstructorProps) {
        this._baseUrl = props?.baseUrl ?? '';

        this._charts = {
            discoverChartList: (option?: IKaolistChartsListApiOption) => {
                const queryBuild = (options?: IKaolistChartsListApiOption): QueryType<IKaolistChartsListApiOption> | undefined => {
                    if (options === undefined || Object.keys(options).length === 0) {
                        return;
                    }

                    const { date, startDate, endDate, ...rest } = options;
                    let q: QueryType<IKaolistChartsListApiOption> = { ...rest };

                    if (startDate) {
                        q.startDate = startDate.toISOString();
                    }

                    if (endDate) {
                        q.endDate = endDate.toISOString();
                    }

                    return q;
                }

                return this.getAsync(kaoListApiEndPoint.chartDiscover, queryBuild(option)).then(item => item.json() as Promise<IDiscoverChartListResponse>);
            },

            likedChartList: (option?: IKaolistChartsListApiOption) => {
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

                return this.getAsync(kaoListApiEndPoint.chartLiked, queryBuild(option)).then(item => item.json() as Promise<ILikedChartListResponse>);
            }
        }

        this._searchs = {
            songSearchList: (option?: IKaolistSearchListApiOption) => {
                const queryBuild = (options?: IKaolistSearchListApiOption): QueryType<IKaolistSearchListApiOption> | undefined => {
                    if (options === undefined || Object.keys(options).length === 0) {
                        return;
                    }

                    const { q, ...rest } = options;
                    let query: QueryType<IKaolistSearchListApiOption> = { ...rest };

                    if (q) {
                        query.q = q;
                    }

                    return query;
                }

                return this.getAsync(kaoListApiEndPoint.searchSong, queryBuild(option)).then(item => item.json() as Promise<ISearchListResponse>);
            }
        }

        this._songs = {
            songDetail: (option?: IKaolistSongDetailApiOption) => {
                const queryBuild = (options?: IKaolistSongDetailApiOption): QueryType<IKaolistSongDetailApiOption> | undefined => {
                    if (options === undefined || Object.keys(options).length === 0) {
                        return;
                    }

                    const { id, ...rest } = options;
                    let query: QueryType<IKaolistSongDetailApiOption> = { ...rest };

                    if (id) {
                        query.id = id;
                    }

                    return query;
                }

                return this.getAsync(kaoListApiEndPoint.songDetail, queryBuild(option)).then(item => item.json() as Promise<ISongDetailResponse>);
            },

            songRate: (option?: IKaolistSongRateApiOption): Promise<ISongRateResponse> => {
                const query = {
                    rating: option?.rate !== undefined ? SongRating[option.rate] : SongRating.None,
                    ids: option?.songId
                };

                return this.putAsync(kaoListApiEndPoint.songRate, null, query);
            },

            songGetRating: (option?: IKaolistSongGetRatingApiOption) => {
                const queryBuild = (options?: IKaolistSongGetRatingApiOption): QueryType<IKaolistSongGetRatingApiOption> | undefined => {
                    if (options === undefined || Object.keys(options).length === 0) {
                        return;
                    }

                    const {ids: singIds, ...rest} = options;
                    let query: QueryType<IKaolistSongGetRatingApiOption> = { ...rest};

                    if (singIds) {
                        query.ids = singIds;
                    }

                    return query;
                }

                return this.getAsync(kaoListApiEndPoint.songGetRating, queryBuild(option)).then(item => item.json() as Promise<ISongGetRatingResponse>);
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

    getAsync = async (path: string, query?: QueryType) => {
        const token = await authorizeService.getAccessToken();
        let init: RequestInit | undefined = undefined;
        if (token !== undefined) {
            init = {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }
        }
        return await fetch(this.buildUrl(path, query), init);
    }

    putAsync = async (path: string, body?: BodyInit | null, query?: QueryType): Promise<ISongRateResponse> => {
        const token = await authorizeService.getAccessToken();
        let init: RequestInit | undefined = undefined;
    
        if (token !== undefined) {
            init = {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
                body: body ? JSON.stringify(body) : body,
            };
        }
    
        const response = await fetch(this.buildUrl(path, query), init);
    
        if (response.ok) {
            return { statusCode: response.status };
        } else {
            throw { statusCode: response.status };
        }
    }


    get charts(): IKaolistChartsApi {
        return this._charts;
    }

    get searchs(): IKaolistSearchsApi {
        return this._searchs;
    }

    get songs(): IKaolistSongsApi {
        return this._songs;
    }
}

const api = new KaoListApi();
window.api = {
    ...window.api,
    kaoList: api
}

export default api;
