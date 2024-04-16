import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import LazyNaverIcon from "../../svgs/LazyNaverIcon";
import LazyGoogleIcon from "../../svgs/LazyGoogleIcon";
import LazyKakaoIcon from "../../svgs/LazyKakaoIcon";

interface IMyPageProfileExternalLoginProps {
    isExternal?: boolean;
    socialInfos?: string[];
}

function MyPageProfileExternalLogin({ isExternal = true, socialInfos = [] }: IMyPageProfileExternalLoginProps) {
    const { t } = useTranslation("MyPage");
    const externalInfoSet = React.useMemo(() => new Set(socialInfos), [socialInfos]);

    const handleLinkExternalAccount = (provider: string) => {
        window.location.href = `/link-`
    }

    return (
        <>
            {isExternal ?
                <MyPageItem
                    className="external-login"
                    title={`${t('External login')}`}
                    options={
                        <>
                            {externalInfoSet.has("naver") && <LazyNaverIcon className="naver-icon" />}
                            {externalInfoSet.has("google") && <LazyGoogleIcon className="google-icon" />}
                            {externalInfoSet.has("kakao") && <LazyKakaoIcon className="kakao-icon" />}
                        </>
                    }
                />
                :
                <MyPageItem
                    className="external-login"
                    title={`${t('External login')}`}
                />
            }
        </>
    )
}

export default React.memo(MyPageProfileExternalLogin);