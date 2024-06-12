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
    myPageProfile = (option?: IKaolistMyPageApiOption): Promise<IMyPageProfileResponse> => {
        return this.getAsync(kaoListApiEndPoint.myPageProfile, option).then(item => item.json() as Promise<IMyPageProfileResponse>);
    };

    myPageSongSearchLogList = (option?: IKaolistMyPageApiOption): Promise<ISongSearchLogListResponse> => {
        return this.getAsync(kaoListApiEndPoint.myPageSongSearchLogList, option).then(item => item.json() as Promise<ISongSearchLogListResponse>);
    };

    myPageSignInLogList = (option?: IKaolistMyPageApiOption): Promise<ISignInLogListReseponse> => {
        return this.getAsync(kaoListApiEndPoint.myPageSignInLogList, option).then(item => item.json() as Promise<ISignInLogListReseponse>);
    };

    myPageFollowedSongList = (option?: IKaolistMyPageApiOption): Promise<IFollowedSongListResponse> => {
        return this.getAsync(kaoListApiEndPoint.myPageFollowedSongList, option).then(item => item.json() as Promise<IFollowedSongListResponse>);
    };

    myPageSetProfileImage = (properties: IKaolistMyPageSetProfileImageApiProperties): Promise<IMyPageSetProfileImageResponse> => {
        const formData = new FormData();
        formData.append("Image", properties.image);

        return this.postAsync(kaoListApiEndPoint.myPageSetProfileImage, formData)
            .then(item => item.json() as IMyPageSetProfileImageResponse);
    };

    myPageGetProfileImage = (properties?: IKaolistMyPageGetProfileImageApiProperties): Promise<IMyPageGetProfileImageResponse> => {
        return this.getAsync(kaoListApiEndPoint.myPageGetProfileImage, properties).then(item => item.json() as Promise<IMyPageGetProfileImageResponse>);
    };

    myPageSetNickname = (properties: IKaolistMyPageSetNicknameApiProperties): Promise<IMyPageSetNicknameResponse> => {
        return this.postAsync(kaoListApiEndPoint.myPageSetNickname, properties)
            .then(response => {
                if (response.ok) {
                    return response.json() as Promise<{ message: string }>;
                } else {
                    throw new Error("Nickname update failed");
                }
            });
    };
}

export default KaoListMyPagesApi;
