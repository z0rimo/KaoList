import { IKaolistSongsApi, IKaolistSongDetailApiOption, IKaolistSongRateApiOption, IKaolistSongGetRatingApiOption, ISongDetailResponse, ISongRateResponse, ISongGetRatingResponse } from "../models/ISongModels";
import { SongRating } from "../enums/SongRating";
import ApiServiceBase from "../base/ApiServiceBase";

const kaoListApiEndPoint = {
    songDetail: '/api/songs/detail',
    songRate: 'api/songs/rate',
    songGetRating: 'api/songs/getRating',
}

export class KaoListSongsApi extends ApiServiceBase implements IKaolistSongsApi {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    songDetail = (option?: IKaolistSongDetailApiOption): Promise<ISongDetailResponse> => {
        const specialHandler = (options: IKaolistSongDetailApiOption) => {
            const { id, ...rest } = options;
            let query: Partial<IKaolistSongDetailApiOption> = { ...rest };
            if (id) {
                query.id = id;
            }

            return query;
        };

        const query = specialHandler(option || {});
        return this.getAsync(kaoListApiEndPoint.songDetail, query).then(item => item.json() as Promise<ISongDetailResponse>);
    };

    songRate = (option?: IKaolistSongRateApiOption): Promise<ISongRateResponse> => {
        const query = {
            rating: option?.rate !== undefined ? SongRating[option.rate] : SongRating.None,
            ids: option?.songId
        };

        return this.putJsonAsync(kaoListApiEndPoint.songRate, query);
    };

    songGetRating = (option?: IKaolistSongGetRatingApiOption): Promise<ISongGetRatingResponse> => {
        const specialHandler = (options: IKaolistSongGetRatingApiOption) => {
            const { ids, ...rest } = options;
            let query: Partial<IKaolistSongGetRatingApiOption> = { ...rest };
            if (ids) {
                query.ids = ids;
            }

            return query;
        };

        const query = specialHandler(option || {});
        return this.getAsync(kaoListApiEndPoint.songGetRating, query).then(item => item.json() as Promise<ISongGetRatingResponse>);
    };
}
