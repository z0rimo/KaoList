import { ExternalLogin } from "../../enums/ExternalLogin";
import { QueryType } from "../base/ApiServiceBase";
import { IApiGlobalOption, IKaoListResponse } from "./IApiResponse";

export interface IMyPageSongSearchLogResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    query?: string;
    created?: string;
}

export interface IMyPageSignInLogResource extends IKaoListResponse {
    kind?: string;
    id: number;
    created?: string;
    ipAddress?: string;
}

export interface IMyPageFollowedSongResource extends IKaoListResponse {
    kind?: string;
    id?: string;
    title?: string;
    created?: string;
}

export interface ISongSearchLogListResponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageSongSearchLogResource[];
}

export interface ISignInLogListReseponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageSignInLogResource[];
}

export interface IFollowedSongListResponse extends IKaoListResponse {
    kind?: string;
    resources?: IMyPageFollowedSongResource[];
}

export interface IMyPageProfileResource extends IKaoListResponse {
    id?: string;
    email?: string;
    nickname?: string;
    nicknameEditedDateTime?: Date;
    externalLogin?: ExternalLogin;
}

export interface IMyPageProfileResponse extends IKaoListResponse {
    resource?: IMyPageProfileResource;
}

export interface IMyPageSetProfileImageResponse {
    statusCode?: number;
    imageUrl?: string;
}

export interface IMyPageGetProfileImageResponse {
    imageUrl?: string;
}

export interface IMyPageSetNicknameResponse {
    statusCode?: number;
    message?: string;
    errors?: any;
}

export interface IKaolistMyPageApiOption extends QueryType<IApiGlobalOption> {}

export interface IKaolistMyPageSetProfileImageApiProperties {
    image: File;
}

export interface IKaolistMyPageGetProfileImageApiProperties extends QueryType<{ id?: string }> {}

export interface IKaolistMyPageSetNicknameApiProperties {
    nickname: string;
}

export interface IKaolistMyPagesApi {
    myPageProfile: (option?: IKaolistMyPageApiOption) => Promise<IMyPageProfileResponse>;
    myPageSongSearchLogList: (option?: IKaolistMyPageApiOption) => Promise<ISongSearchLogListResponse>;
    myPageSignInLogList: (option?: IKaolistMyPageApiOption) => Promise<ISignInLogListReseponse>;
    myPageFollowedSongList: (option?: IKaolistMyPageApiOption) => Promise<IFollowedSongListResponse>;
    myPageSetProfileImage: (properties: IKaolistMyPageSetProfileImageApiProperties) => Promise<IMyPageSetProfileImageResponse>;
    myPageGetProfileImage: (properties: IKaolistMyPageGetProfileImageApiProperties) => Promise<IMyPageGetProfileImageResponse>;
    myPageSetNickname: (properties: IKaolistMyPageSetNicknameApiProperties) => Promise<IMyPageSetNicknameResponse>;
}