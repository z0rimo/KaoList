import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import SwitchButton from "../SwitchButton/SwitchButton";

function MyPageAgreeEmail() {
    const { t } = useTranslation("MyPage");

    return (
        <MyPageItem
            title={`${t('Agree to receive email')}`}
            options={<SwitchButton />}
        />
    )
}

export default React.memo(MyPageAgreeEmail);