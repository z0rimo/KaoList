import { IApiGlobalOption, IKaoListPageResponse, IKaoListResponse } from "./IApiResponse";
import { ISongSnippet } from "./ISongModels";

export interface ISearchSongId {
    kind?: string;
    id?: string;
}

export interface ISearchSnippet extends ISongSnippet {}

export interface ISearchResource extends IKaoListResponse {
    kind?: string;
    id?: ISearchSongId;
    snippet?: ISearchSnippet[];
}

export interface ISearchListResponse extends IKaoListPageResponse {
    kind?: string;
    items?: ISearchResource[];
}

export interface IKaolistSearchListApiOption extends IApiGlobalOption {
    q?: string[];
    page?: number;
}

export interface IKaolistSearchsApi {
    songSearchList: (option?: IKaolistSearchListApiOption) => Promise<ISearchListResponse>;
}
