import { IKaolistApi, IKaolistApiConstructorProps } from "./models/IKaoListApiModels";
import KaoListChartsApi from "./services/KaoListChartsApi";
import KaoListLogsApi from "./services/KaoListLogsApi";
import KaoListMyPagesApi from "./services/KaoListMyPagesApi";
import KaoListSearchsApi from "./services/KaoListSearchsApi";
import KaoListSongsApi from "./services/KaoListSongsApi";

declare global {
    interface Window {
        api: {
            kaoList: IKaolistApi;
        };
    }
}

class KaoListApi implements IKaolistApi {
    private _baseUrl: string;

    private _charts: KaoListChartsApi;
    private _searchs: KaoListSearchsApi;
    private _songs: KaoListSongsApi;
    private _myPages: KaoListMyPagesApi;
    private _logs: KaoListLogsApi;

    constructor(props?: IKaolistApiConstructorProps) {
        this._baseUrl = props?.baseUrl ?? '';

        this._charts = new KaoListChartsApi(this._baseUrl);
        this._searchs = new KaoListSearchsApi(this._baseUrl);
        this._songs = new KaoListSongsApi(this._baseUrl);
        this._myPages = new KaoListMyPagesApi(this._baseUrl);
        this._logs = new KaoListLogsApi(this._baseUrl);
    }

    get charts() {
        return this._charts;
    }

    get searchs() {
        return this._searchs;
    }

    get songs() {
        return this._songs;
    }

    get mypages() {
        return this._myPages;
    }

    get logs() {
        return this._logs;
    }
}

const api = new KaoListApi({ baseUrl: process.env.BASE_URL || "" });

if (!window.api) {
    window.api = {} as any;
}

window.api.kaoList = api;

export default api;