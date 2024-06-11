import { IKaolistSearchsApi, IKaolistSearchListApiOption, ISearchListResponse } from "../models/ISearchModels";
import ApiServiceBase from "../base/ApiServiceBase";

const kaoListApiEndPoint = {
    searchSong: '/api/search/list',
}

export class KaoListSearchsApi extends ApiServiceBase implements IKaolistSearchsApi {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    songSearchList = (option?: IKaolistSearchListApiOption): Promise<ISearchListResponse> => {
        const specialHandler = (options: IKaolistSearchListApiOption) => {
            const { q, ...rest } = options;
            let query: Partial<IKaolistSearchListApiOption> = { ...rest };

            if (q) {
                query.q = q;
            }

            return query;
        };

        const query = specialHandler(option || {});
        return this.getAsync(kaoListApiEndPoint.searchSong, query).then(item => item.json() as Promise<ISearchListResponse>);
    };
}
