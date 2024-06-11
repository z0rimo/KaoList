import { IKaolistChartsApi } from "./IChartModels";
import { IKaolistSearchsApi } from "./ISearchModels";
import { IKaolistSongsApi } from "./ISongModels";
import { IKaolistMyPagesApi } from "./IMyPageModels";

export interface IKaolistApi {
    charts: IKaolistChartsApi;
    searchs: IKaolistSearchsApi;
    songs: IKaolistSongsApi;
    mypages: IKaolistMyPagesApi;
}

export interface IKaolistApiConstructorProps {
    baseUrl: string;
}