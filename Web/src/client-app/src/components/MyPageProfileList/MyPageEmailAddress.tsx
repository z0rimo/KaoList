import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";

interface IMyPageEmailAddressItem {
    email?: string;
}

function MyPageEmailAddress({ email = "default@default.com" }: IMyPageEmailAddressItem) {
    const { t } = useTranslation("MyPage");

    return (
        <MyPageItem
            title={`${t('Email address')}`}
            information={email}
        />
    )
}

export default React.memo(MyPageEmailAddress);