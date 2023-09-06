import React from "react";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import MyPageItem from "../mypage/MyPageItem";

function MyPageChangePassword() {
    const { t } = useTranslation("MyPage");

    return (
        <MyPageItem
            title={`${t('Change password')}`}
            options={
                <Link to="">
                    {t('Change password')}
                </Link>
            }
        />
    )
}

export default React.memo(MyPageChangePassword);