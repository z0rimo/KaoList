import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import LazyNaverIcon from "../../svgs/LazyNaverIcon";

interface IMyPageProfileExternalLoginProps {
    isExternal?: boolean;
    socialInfos?: string[];
}

function MyPageProfileExternalLogin({ isExternal = true, socialInfos = ["naver"] }: IMyPageProfileExternalLoginProps) {
    const { t } = useTranslation("MyPage");
    const externalInfoSet = React.useMemo(() => new Set(socialInfos), [socialInfos]);

    return (
        <>
            {isExternal ?
                <MyPageItem
                    className="external-login"
                    title={`${t('External login')}`}
                    options={externalInfoSet.has("naver") && <LazyNaverIcon className="naver-icon" />}
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