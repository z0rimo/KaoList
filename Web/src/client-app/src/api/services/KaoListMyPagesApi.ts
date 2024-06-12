import kaoListApiEndPoint from "../KaoListApiEndPoint";
import { ApiServiceBase } from "../base/ApiServiceBase";
import {
    IMyPageProfileResponse,
    ISongSearchLogListResponse,
    ISignInLogListReseponse,
    IFollowedSongListResponse,
    IMyPageSetProfileImageResponse,
    IMyPageGetProfileImageResponse,
    IMyPageSetNicknameResponse,
    IKaolistMyPageApiOption,
    IKaolistMyPageSetProfileImageApiProperties,
    IKaolistMyPageGetProfileImageApiProperties,
    IKaolistMyPageSetNicknameApiProperties,
} from "../models/IMyPageModels";

class KaoListMyPagesApi extends ApiServiceBase {
    myPageProfile = async (option?: IKaolistMyPageApiOption): Promise<IMyPageProfileResponse> => {
        const item = await this.getAsync(kaoListApiEndPoint.myPageProfile, option);
        return await (item.json() as Promise<IMyPageProfileResponse>);
    };

    myPageSongSearchLogList = async (option?: IKaolistMyPageApiOption): Promise<ISongSearchLogListResponse> => {
        const item = await this.getAsync(kaoListApiEndPoint.myPageSongSearchLogList, option);
        return await (item.json() as Promise<ISongSearchLogListResponse>);
    };

    myPageSignInLogList = async (option?: IKaolistMyPageApiOption): Promise<ISignInLogListReseponse> => {
        const item = await this.getAsync(kaoListApiEndPoint.myPageSignInLogList, option);
        return await (item.json() as Promise<ISignInLogListReseponse>);
    };

    myPageFollowedSongList = async (option?: IKaolistMyPageApiOption): Promise<IFollowedSongListResponse> => {
        const item = await this.getAsync(kaoListApiEndPoint.myPageFollowedSongList, option);
        return await (item.json() as Promise<IFollowedSongListResponse>);
    };

    myPageSetProfileImage = async (properties: IKaolistMyPageSetProfileImageApiProperties): Promise<IMyPageSetProfileImageResponse> => {
        const formData = new FormData();
        formData.append("Image", properties.image);

        const item = await this.postAsync(kaoListApiEndPoint.myPageSetProfileImage, formData);
        return item.json() as IMyPageSetProfileImageResponse;
    };

    myPageGetProfileImage = async (properties?: IKaolistMyPageGetProfileImageApiProperties): Promise<IMyPageGetProfileImageResponse> => {
        const item = await this.getAsync(kaoListApiEndPoint.myPageGetProfileImage, properties);
        return await (item.json() as Promise<IMyPageGetProfileImageResponse>);
    };

    myPageSetNickname = async (properties: IKaolistMyPageSetNicknameApiProperties): Promise<IMyPageSetNicknameResponse> => {
        const response = await this.postAsync(kaoListApiEndPoint.myPageSetNickname, properties);
        if (response.ok) {
            return response.json() as Promise<{ message: string; }>;
        } else {
            throw new Error("Nickname update failed");
        }
    };
}

export default KaoListMyPagesApi;
