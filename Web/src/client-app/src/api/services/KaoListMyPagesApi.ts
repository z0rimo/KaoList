import { IKaolistMyPagesApi, IKaolistMyPageApiOption, IKaolistMyPageSetProfileImageApiProperties, IKaolistMyPageGetProfileImageApiProperties, IKaolistMyPageSetNicknameApiProperties, IMyPageProfileResponse, ISongSearchLogListResponse, ISignInLogListReseponse, IFollowedSongListResponse, IMyPageSetProfileImageResponse, IMyPageGetProfileImageResponse, IMyPageSetNicknameResponse } from "../models/IMyPageModels";
import ApiServiceBase from "../base/ApiServiceBase";

const kaoListApiEndPoint = {
    myPageProfile: 'api/mypage/profile',
    myPageSongSearchLogList: 'api/mypage/songSearchLogList',
    myPageSignInLogList: 'api/mypage/signInLogList',
    myPageFollowedSongList: 'api/mypage/followedSongList',
    myPageSetProfileImage: 'api/mypage/setProfileImage',
    myPageGetProfileImage: 'api/mypage/getProfileImage',
    myPageSetNickname: 'api/mypage/setNickname'
}

export class KaoListMyPagesApi extends ApiServiceBase implements IKaolistMyPagesApi {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    myPageProfile = (option?: IKaolistMyPageApiOption): Promise<IMyPageProfileResponse> => {
        const query = option;
        return this.getAsync(kaoListApiEndPoint.myPageProfile, query).then(item => item.json() as Promise<IMyPageProfileResponse>);
    };

    myPageSongSearchLogList = (option?: IKaolistMyPageApiOption): Promise<ISongSearchLogListResponse> => {
        const query = option;
        return this.getAsync(kaoListApiEndPoint.myPageSongSearchLogList, query).then(item => item.json() as Promise<ISongSearchLogListResponse>);
    };

    myPageSignInLogList = (option?: IKaolistMyPageApiOption): Promise<ISignInLogListReseponse> => {
        const query = option;
        return this.getAsync(kaoListApiEndPoint.myPageSignInLogList, query).then(item => item.json() as Promise<ISignInLogListReseponse>);
    };

    myPageFollowedSongList = (option?: IKaolistMyPageApiOption): Promise<IFollowedSongListResponse> => {
        const query = option;
        return this.getAsync(kaoListApiEndPoint.myPageFollowedSongList, query).then(item => item.json() as Promise<IFollowedSongListResponse>);
    };

    myPageSetProfileImage = (properties: IKaolistMyPageSetProfileImageApiProperties): Promise<IMyPageSetProfileImageResponse> => {
        const formData = new FormData();
        formData.append("Image", properties.image);

        return this.postAsync(kaoListApiEndPoint.myPageSetProfileImage, formData)
            .then(item => item.json() as IMyPageSetProfileImageResponse);
    };

    myPageGetProfileImage = (properties?: IKaolistMyPageGetProfileImageApiProperties): Promise<IMyPageGetProfileImageResponse> => {
        const query = properties;
        return this.getAsync(kaoListApiEndPoint.myPageGetProfileImage, query).then(item => item.json() as Promise<IMyPageGetProfileImageResponse>);
    };

    myPageSetNickname = (properties: IKaolistMyPageSetNicknameApiProperties): Promise<IMyPageSetNicknameResponse> => {
        return this.postJsonAsync(kaoListApiEndPoint.myPageSetNickname, properties)
            .then(response => {
                if (response.ok) {
                    return response.json() as Promise<{ message: string }>;
                } else {
                    throw new Error("Nickname update failed");
                }
            });
    };
}
