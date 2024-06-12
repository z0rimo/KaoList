import { IKaolistApi, IKaolistApiConstructorProps } from "./models/IKaoListApiModels";
import KaoListChartsApi from "./services/KaoListChartsApi";
import KaoListMyPagesApi from "./services/KaoListMyPagesApi";
import KaoListSearchsApi from "./services/KaoListSearchsApi";
import KaoListSongsApi from "./services/KaoListSongsApi";

class KaoListApi implements IKaolistApi {
    private _baseUrl: string;

    private _charts: KaoListChartsApi;
    private _searchs: KaoListSearchsApi;
    private _songs: KaoListSongsApi;
    private _myPages: KaoListMyPagesApi;

    constructor(props?: IKaolistApiConstructorProps) {
        this._baseUrl = props?.baseUrl ?? '';

        this._charts = new KaoListChartsApi(this._baseUrl);
        this._searchs = new KaoListSearchsApi(this._baseUrl);
        this._songs = new KaoListSongsApi(this._baseUrl);
        this._myPages = new KaoListMyPagesApi(this._baseUrl);
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
}

const api = new KaoListApi({ baseUrl: process.env.BASE_URL || "" });
window.api = {
    ...window.api,
    kaoList: api
};

export default api;
