import kaoListApiEndPoint from "../KaoListApiEndPoint";
import ApiServiceBase from "../base/ApiServiceBase";
import { IAppLog } from "../models/ILogModels";

class KaoListLogsApi extends ApiServiceBase {
    sendAppLog = async (logMessage?: string): Promise<IAppLog> => {
        const logData: IAppLog = { log: logMessage };
        const response = await this.postJsonAsync(kaoListApiEndPoint.appLog, logData);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json() as IAppLog;
    };
}

export default KaoListLogsApi;