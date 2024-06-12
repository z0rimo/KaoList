import kaoListApiEndPoint from "../KaoListApiEndPoint";
import ApiServiceBase from "../base/ApiServiceBase";
import { IDiscoverChartListResponse, IKaolistChartsApi, IKaolistChartsListApiOption, ILikedChartListResponse } from "../models/IChartModels";

export class KaoListChartsApi extends ApiServiceBase implements IKaolistChartsApi {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    discoverChartList = async (option?: IKaolistChartsListApiOption): Promise<IDiscoverChartListResponse> => {
        const specialHandler = (options: IKaolistChartsListApiOption) => {
            const { date, ...rest } = options;
            let q: Partial<IKaolistChartsListApiOption> = { ...rest };

            if (date instanceof Date) {
                q.date = date;
            }

            return q;
        };

        const query = specialHandler(option || {});
        const item = await this.getAsync(kaoListApiEndPoint.chartDiscover, query);
      return await (item.json() as Promise<IDiscoverChartListResponse>);
    };

    likedChartList = async (option?: IKaolistChartsListApiOption): Promise<ILikedChartListResponse> => {
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

        const query = specialHandler(option || {});
        const item = await this.getAsync(kaoListApiEndPoint.chartLiked, query);
      return await (item.json() as Promise<ILikedChartListResponse>);
    };
}

export default KaoListChartsApi;