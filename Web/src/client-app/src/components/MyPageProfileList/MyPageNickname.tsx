import React from "react";
import { useTranslation } from "react-i18next";
import MyPageItem from "../mypage/MyPageItem";
import LazyPencilIcon from "../../svgs/LazyPencilIcon";
import DateOptionFormatter from "../DateOptionFormatter";

interface IMyPageProfileNicknameItem {
    nickname?: string;
    lastModified?: Date;
}

function MyPageNickname(
    {
        nickname = "고흥군청소년문화의집",
        lastModified = new Date(2007, 1, 28, 11, 39, 7)
    }: IMyPageProfileNicknameItem) {
    const { t } = useTranslation("MyPage");

    return (
        <MyPageItem
            title={`${t('Nickname')}`}
            information={
                <div className="information-wrapper">
                    <p className="information">{nickname}</p>
                    <span className="fs-8 more-information">
                        <p>{`${t('Last modified')}:`}</p>
                        <p>{lastModified.toLocaleDateString(navigator.language, DateOptionFormatter.long)}</p>
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