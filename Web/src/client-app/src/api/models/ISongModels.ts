import { SongRating } from "../../enums/SongRating";
import { IKaoListResponse, IKaoListPageResponse, IApiGlobalOption } from "./IApiResponse";
import { II18nLanguageResource } from "./II18nModels";

export interface IThumbnailResource {
    url?: string;
    width?: number;
    height?: number;
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

export interface IKaolistSongsApi {
    songDetail: (option?: IKaolistSongDetailApiOption) => Promise<ISongDetailResponse>;
    songRate: (option?: IKaolistSongRateApiOption) => Promise<ISongRateResponse>;
    songGetRating: (option?: IKaolistSongGetRatingApiOption) => Promise<ISongGetRatingResponse>;
}
