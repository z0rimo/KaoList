import { IApiGlobalOption, IKaoListPageResponse, IKaoListResponse } from "./IApiResponse";
import { ISongSnippet } from "./ISongModels";

export interface IDiscoverChartResource {
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

export interface IKaolistChartsListApiOption extends IApiGlobalOption {
    startDate?: Date;
    endDate?: Date;
    date?: Date;
    page?: number;
}

export interface IKaolistChartsApi {
    discoverChartList: (option?: IKaolistChartsListApiOption) => Promise<IDiscoverChartListResponse>;
    likedChartList: (option?: IKaolistChartsListApiOption) => Promise<ILikedChartListResponse>;
}
