import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import LazyPencilIcon from "../../svgs/LazyPencilIcon";

interface IMyPageEmailAddressItem {
    information?: string;
}

function MyPageEmailAddress({ information = "pfcgm24@naver.com" }: IMyPageEmailAddressItem) {
    const { t } = useTranslation("MyPage");

    return (
        <MyPageItem
            title={`${t('Email address')}`}
            information={information}
            options={
                <button>
                    <LazyPencilIcon className="desaturated-cyan-icon" />
                </button>
            }
        />
    )
}

export default React.memo(MyPageEmailAddress);