import authorizeService from "../api-authorization/AuthorizeService";
import { SongRating } from "../enums/SongRating";
import { ExternalLogin } from "../enums/ExternalLogin";
import { queryBuildHelper } from "../queryBuildHelper";

declare global {
    interface Window {
        api: {
            kaoList: IKaolistApi
        }
    }
}

export interface IKaoListResponse {
    kind?: string;
    etag?: string;
}

export interface IPageInfo {
    totalResults: number;
    resultsPerPage: number;
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
    kind?: string;
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
    created?: string;
    title?: string;
    songUsers?: ISongUser[];
    composer?: string;
    thumbnail?: IThumbnailResource;
    i18nName?: II18nLanguageResource;
    karaoke?: ISongKaraoke;
}

export interface ISongResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    rating?: SongRating;
    snippet?: ISongSnippet;
    statistics?: ISongStatistics;
}

export interface ISongListResponse extends IKaoListPageResponse {
    kind?: string;
    items?: ISongResource[];
}

export interface IDiscoverChartResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    snippet?: ISongSnippet;
}

export interface IDiscoverChartListResponse extends IKaoListPageResponse {
    kind?: string;
    resources?: IDiscoverChartResource[];
}

export interface ILikedChartResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    snippet?: ISongSnippet;
}

export interface ILikedChartListResponse extends IKaoListPageResponse {
    kind?: string;
    resources?: ILikedChartResource[];
}

export interface ISearchSongId {
    kind?: string;
    id?: string;
}

export interface ISearchSnippet extends ISongSnippet {

}

export interface ISearchResource extends IKaoListResponse {
    kind?: string;
    id?: ISearchSongId;
    snippet?: ISearchSnippet[];
}

export interface ISearchListResponse extends IKaoListPageResponse {
    kind?: string;
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
    kind?: string;
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

export interface IMyPageSongSearchLogResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    query?: string;
    created?: string;
}

export interface IMyPageSignInLogResource extends IKaoListResponse {
    kind?: string;
    id: number;
    created?: string;
    ipAddress?: string;
}

export interface IMyPageFollowedSongResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    title?: string;
    created?: string;
}

export interface ISongSearchLogListResponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageSongSearchLogResource[];
}

export interface ISignInLogListReseponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageSignInLogResource[];
}

export interface IFollowedSongListResponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageFollowedSongResource[];
}

export interface IMyPageProfileResource extends IKaoListResponse {
    id?: string;
    email?: string;
    nickname?: string;
    nicknameEditedDateTime?: Date;
    externalLogin?: ExternalLogin;
}

export interface IMyPageProfileResponse extends IKaoListResponse {
    resource?: IMyPageProfileResource;
}

export interface IMyPageSetProfileImageResponse {
    statusCode?: number;
}

export interface IMyPageGetProfileImageResponse {
    imageUrl?: string;
}

export interface IMyPageSetNicknameResponse {
    statusCode?: number;
    message?: string;
    errors?: any;
}

export interface IApiGlobalOption {
    part?: Array<'snippet'>;
    offset?: number;
    maxResults?: number;
}

export interface IKaolistChartsListApiOption extends IApiGlobalOption {
    startDate?: Date;
    endDate?: Date;
    date?: Date;
    page?: number;
}

export interface IKaolistSearchListApiOption extends IApiGlobalOption {
    q?: string[];
    page?: number;
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

export interface IKaolistMyPageApiOption extends IApiGlobalOption {
}

export interface IKaolistMyPageSetProfileImageApiProperties {
    image: File;
}

export interface IKaolistMyPageGetProfileImageApiProperties {
    id?: string;
}

export interface IKaolistMyPageSetNicknameApiProperties {
    nickname: string;
}

export type QueryType<T extends object = { [key: string]: string | number | string[] | number[] | Date }> = {
    [P in keyof T]?: T[P] extends (string | number | string[] | number[] | Date | undefined) ? T[P] : string | number | string[] | number[] | Date;
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

interface IKaolistMyPagesApi {
    myPageProfile: (option?: IKaolistMyPageApiOption) => Promise<IMyPageProfileResponse>;
    myPageSongSearchLogList: (option?: IKaolistMyPageApiOption) => Promise<ISongSearchLogListResponse>;
    myPageSignInLogList: (option?: IKaolistMyPageApiOption) => Promise<ISignInLogListReseponse>;
    myPageFollowedSongList: (option?: IKaolistMyPageApiOption) => Promise<IFollowedSongListResponse>;
    myPageSetProfileImage: (properties: IKaolistMyPageSetProfileImageApiProperties) => Promise<IMyPageSetProfileImageResponse>;
    myPageGetProfileImage: (properties: IKaolistMyPageGetProfileImageApiProperties) => Promise<IMyPageGetProfileImageResponse>;
    myPageSetNickname: (properties: IKaolistMyPageSetNicknameApiProperties) => Promise<IMyPageSetNicknameResponse>;
}

interface IKaolistApi {
    charts: IKaolistChartsApi;
    searchs: IKaolistSearchsApi;
    songs: IKaolistSongsApi;
    mypages: IKaolistMyPagesApi;
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
    myPageProfile: 'api/mypage/profile',
    myPageSongSearchLogList: 'api/mypage/songSearchLogList',
    myPageSignInLogList: 'api/mypage/signInLogList',
    myPageFollowedSongList: 'api/mypage/followedSongList',
    myPageSetProfileImage: 'api/mypage/setProfileImage',
    myPageGetProfileImage: 'api/mypage/getProfileImage',
    myPageSetNickname: 'api/mypage/setNickname'
}

class KaoListApi implements IKaolistApi {
    private _baseUrl: string;

    private _charts: IKaolistChartsApi;
    private _searchs: IKaolistSearchsApi;
    private _songs: IKaolistSongsApi;
    private _myPages: IKaolistMyPagesApi;

    constructor(props?: IKaolistApiConstructorProps) {
        this._baseUrl = props?.baseUrl ?? '';

        this._charts = {
            discoverChartList: (option?: IKaolistChartsListApiOption) => {
                const specialHandler = (options: IKaolistChartsListApiOption) => {
                    const { date, ...rest } = options;
                    let q: Partial<IKaolistChartsListApiOption> = { ...rest };

                    if (date instanceof Date) {
                        q.date = date;
                    }

                    return q;
                };

                const query = queryBuildHelper(option, specialHandler);

                return this.getAsync(kaoListApiEndPoint.chartDiscover, query).then(item => item.json() as Promise<IDiscoverChartListResponse>);
            },

            likedChartList: (option?: IKaolistChartsListApiOption) => {
                const specialHandler = (options: IKaolistChartsListApiOption) => {
                    const { startDate, endDate, ...rest } = options;
                    let q: Partial<IKaolistChartsListApiOption> = { ...rest };

                    if (startDate instanceof Date) {
                        q.startDate = startDate;
                    }

                    if (endDate instanceof Date) {
                        q.endDate = endDate;
                    }

                    return q;
                }

                const query = queryBuildHelper(option, specialHandler);

                return this.getAsync(kaoListApiEndPoint.chartLiked, query).then(item => item.json() as Promise<ILikedChartListResponse>);
            }
        }

        this._searchs = {
            songSearchList: (option?: IKaolistSearchListApiOption) => {
                const specialHandler = (options: IKaolistSearchListApiOption) => {
                    const { q, ...rest } = options;
                    let query: Partial<IKaolistSearchListApiOption> = { ...rest };

                    if (q) {
                        query.q = q;
                    }

                    return query;
                };

                const query = queryBuildHelper(option, specialHandler);
                return this.getAsync(kaoListApiEndPoint.searchSong, query).then(item => item.json() as Promise<ISearchListResponse>);
            }
        }

        this._songs = {
            songDetail: (option?: IKaolistSongDetailApiOption) => {
                const specialHandler = (options: IKaolistSongDetailApiOption) => {
                    const { id, ...rest } = options;
                    let query: Partial<IKaolistSongDetailApiOption> = { ...rest };
                    if (id) {
                        query.id = id;
                    }

                    return query;
                };

                const query = queryBuildHelper(option, specialHandler);

                return this.getAsync(kaoListApiEndPoint.songDetail, query).then(item => item.json() as Promise<ISongDetailResponse>);
            },

            songRate: (option?: IKaolistSongRateApiOption): Promise<ISongRateResponse> => {
                const query = {
                    rating: option?.rate !== undefined ? SongRating[option.rate] : SongRating.None,
                    ids: option?.songId
                };

                return this.putAsync(kaoListApiEndPoint.songRate, null, query);
            },

            songGetRating: (option?: IKaolistSongGetRatingApiOption) => {
                const specialHandler = (options: IKaolistSongGetRatingApiOption) => {
                    const { ids, ...rest } = options;
                    let query: Partial<IKaolistSongGetRatingApiOption> = { ...rest };
                    if (ids) {
                        query.ids = ids;
                    }

                    return query;
                };

                const query = queryBuildHelper(option, specialHandler);

                return this.getAsync(kaoListApiEndPoint.songGetRating, query).then(item => item.json() as Promise<ISongGetRatingResponse>);
            }
        }

        this._myPages = {
            myPageProfile: (option?: IKaolistMyPageApiOption) => {
                const query = queryBuildHelper(option);

                return this.getAsync(kaoListApiEndPoint.myPageProfile, query).then(item => item.json() as Promise<IMyPageProfileResponse>);
            },

            myPageSongSearchLogList: (option?: IKaolistMyPageApiOption) => {
                const query = queryBuildHelper(option);

                return this.getAsync(kaoListApiEndPoint.myPageSongSearchLogList, query).then(item => item.json() as Promise<ISongSearchLogListResponse>);
            },

            myPageSignInLogList: (option?: IKaolistMyPageApiOption) => {
                const query = queryBuildHelper(option);

                return this.getAsync(kaoListApiEndPoint.myPageSignInLogList, query).then(item => item.json() as Promise<ISignInLogListReseponse>);
            },

            myPageFollowedSongList: (option?: IKaolistMyPageApiOption) => {
                const query = queryBuildHelper(option);

                return this.getAsync(kaoListApiEndPoint.myPageFollowedSongList, query).then(item => item.json() as Promise<IFollowedSongListResponse>);
            },

            myPageSetProfileImage: (properties: IKaolistMyPageSetProfileImageApiProperties) => {
                const formData = new FormData();
                formData.append("Image", properties.image);

                return this.postAsync(kaoListApiEndPoint.myPageSetProfileImage, formData)
                    .then(item => item.json() as IMyPageSetProfileImageResponse);
            },

            myPageGetProfileImage: (properties?: IKaolistMyPageGetProfileImageApiProperties) => {
                const query = queryBuildHelper(properties);

                return this.getAsync(kaoListApiEndPoint.myPageGetProfileImage, query).then(item => item.json() as Promise<IMyPageGetProfileImageResponse>);
            },

            myPageSetNickname: (properties: IKaolistMyPageSetNicknameApiProperties) => {
                return this.postAsync(kaoListApiEndPoint.myPageSetNickname, properties)
                    .then(response => {
                        if (response.ok) {
                            return response.json() as Promise<{ message: string }>;
                        } else {
                            throw new Error("Nickname update failed");
                        }
                    });
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

    putAsync = async (path: string, body?: BodyInit | null, query?: QueryType) => {
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

    // postAsync = async (path: string, body?: BodyInit | null, query?: QueryType) => {
    //     const token = await authorizeService.getAccessToken();
    //     let init: RequestInit | undefined = undefined;

    //     if (token !== undefined) {
    //         init = {
    //             method: 'POST',
    //             headers: {
    //                 'Authorization': `Bearer ${token}`,
    //             },
    //             body: body,
    //         };

    //         // If the body is not FormData, set the Content-Type header
    //         if (!(body instanceof FormData)) {
    //             init.headers = {
    //                 ...init.headers,
    //                 'Content-Type': 'application/json',
    //             };
    //             init.body = JSON.stringify(body);
    //         }
    //     }

    //     return await fetch(this.buildUrl(path, query), init);
    // };

    postAsync = async (path: string, body?: object | BodyInit | null, query?: QueryType) => {
        const token = await authorizeService.getAccessToken();

        const headers = new Headers();
        if (token) {
            headers.append('Authorization', `Bearer ${token}`);
        }
        if (!(body instanceof FormData)) {
            headers.append('Content-Type', 'application/json');
        }

        let init: RequestInit = {
            method: 'POST',
            headers: headers,
            body: body instanceof FormData ? body : JSON.stringify(body),
        };

        return await fetch(this.buildUrl(path, query), init);
    };


    get charts(): IKaolistChartsApi {
        return this._charts;
    }

    get searchs(): IKaolistSearchsApi {
        return this._searchs;
    }

    get songs(): IKaolistSongsApi {
        return this._songs;
    }

    get mypages(): IKaolistMyPagesApi {
        return this._myPages;
    }
}

const api = new KaoListApi();
window.api = {
    ...window.api,
    kaoList: api
}

export default api;
