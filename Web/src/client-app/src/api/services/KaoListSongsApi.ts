import ApiServiceBase from "../base/ApiServiceBase";
import kaoListApiEndPoint from "../KaoListApiEndPoint";
import { SongRating } from "../../enums/SongRating";
import {
    IKaolistSongsApi,
    IKaolistSongDetailApiOption,
    IKaolistSongRateApiOption,
    IKaolistSongGetRatingApiOption,
    ISongDetailResponse,
    ISongRateResponse,
    ISongGetRatingResponse
} from "../models/ISongModels";

export class KaoListSongsApi extends ApiServiceBase implements IKaolistSongsApi {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    songDetail = async (option?: IKaolistSongDetailApiOption): Promise<ISongDetailResponse> => {
        const specialHandler = (options: IKaolistSongDetailApiOption) => {
            const { id, ...rest } = options;
            let query: Partial<IKaolistSongDetailApiOption> = { ...rest };
            if (id) {
                query.id = id;
            }

            return query;
        };

        const query = specialHandler(option || {});
        const item = await this.getAsync(kaoListApiEndPoint.songDetail, query);
        return await (item.json() as Promise<ISongDetailResponse>);
    };

    songRate = async (option?: IKaolistSongRateApiOption): Promise<ISongRateResponse> => {
        const query = {
            rating: option?.rate !== undefined ? SongRating[option.rate] : SongRating.None,
            ids: option?.songId
        };

        const response = await this.putJsonAsync(kaoListApiEndPoint.songRate, query);
        return {
            statusCode: response.status,
        };
    };

    songGetRating = async (option?: IKaolistSongGetRatingApiOption): Promise<ISongGetRatingResponse> => {
        const specialHandler = (options: IKaolistSongGetRatingApiOption) => {
            const { ids, ...rest } = options;
            let query: Partial<IKaolistSongGetRatingApiOption> = { ...rest };
            if (ids) {
                query.ids = ids;
            }

            return query;
        };

        const query = specialHandler(option || {});
        const item = await this.getAsync(kaoListApiEndPoint.songGetRating, query);
        return await (item.json() as Promise<ISongGetRatingResponse>);
    };
}

export default KaoListSongsApi;