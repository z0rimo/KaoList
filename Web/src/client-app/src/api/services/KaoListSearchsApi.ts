import ApiServiceBase from "../base/ApiServiceBase";
import kaoListApiEndPoint from "../KaoListApiEndPoint";
import {
    IKaolistSearchsApi,
    IKaolistSearchListApiOption,
    ISearchListResponse
} from "../models/ISearchModels";

export class KaoListSearchsApi extends ApiServiceBase implements IKaolistSearchsApi {
    songSearchList = async (option?: IKaolistSearchListApiOption): Promise<ISearchListResponse> => {
        const specialHandler = (options: IKaolistSearchListApiOption) => {
            const { q, ...rest } = options;
            let query: Partial<IKaolistSearchListApiOption> = { ...rest };

            if (q) {
                query.q = q;
            }

            return query;
        };

        const query = specialHandler(option || {});
        const item = await this.getAsync(kaoListApiEndPoint.searchSong, query);
        return await (item.json() as Promise<ISearchListResponse>);
    };
}

export default KaoListSearchsApi;