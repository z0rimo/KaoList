import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";

function MyPageChangePassword() {
    const { t } = useTranslation("MyPage");
    const handleChangePasswordClick = () => {
        window.location.href = "/Identity/Account/ChangePassword";
    }

    return (
        <MyPageItem
            title={`${t('Change password')}`}
            options={
                <button className="link-item" onClick={handleChangePasswordClick}>
                    {t('Change password')}
                </button>
            }
        />
    )
}

export default React.memo(MyPageChangePassword);