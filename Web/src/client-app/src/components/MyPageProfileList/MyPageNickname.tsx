import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import LazyPencilIcon from "../../svgs/LazyPencilIcon";
import DateOptionFormatter from "../DateOptionFormatter";

interface IMyPageProfileNicknameItem {
    nickname?: string;
    lastModified?: string;
}

function MyPageNickname(
    {
        nickname = "Default Nickname",
        lastModified = new Date().toLocaleDateString()
    }: IMyPageProfileNicknameItem) {
    const { t } = useTranslation("MyPage");
    const dateObject = new Date(lastModified);
    const formattedDate = dateObject.toLocaleDateString(navigator.language, DateOptionFormatter.long);

    return (
        <MyPageItem
            title={`${t('Nickname')}`}
            information={
                <div className="information-wrapper">
                    <p className="information">{nickname}</p>
                    <span className="fs-8 more-information">
                        <p>{`${t('Last modified')}:`}</p>
                        <p>{formattedDate}</p>
                    </span>
                </div>
            }
            options={
                <button>
                    <LazyPencilIcon className="desaturated-cyan-icon pencil-icon" />
                </button>
            }
        />
    )
}

export default React.memo(MyPageNickname);