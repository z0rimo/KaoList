import { IKaoListResponse } from "./IApiResponse";

export interface II18nLanguageSnippet {
    name?: string;
}

export interface II18nLanguageResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    snippet?: II18nLanguageSnippet;
}
