import React, { ChangeEvent, useState } from "react";
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
        lastModified
    }: IMyPageProfileNicknameItem) {
    const { t } = useTranslation(['MyPage', 'Common']);
    const [isEditing, setIsEditing] = useState(false);
    const [editedNickname, setEditedNickname] = useState(nickname)
    const formattedDate = lastModified ? new Date(lastModified).toLocaleDateString(navigator.language, DateOptionFormatter.long) : null;

    const handleEditClick = () => {
        setIsEditing(true);
    };

    const handleCancelClick = () => {
        setIsEditing(false);
        setEditedNickname(nickname);
    }

    const handleSaveClick = async () => {
        setIsEditing(false);
        try {
            const response = await window.api.kaoList.mypages.myPageSetNickname({nickname: editedNickname});
            if (response.statusCode === 200) {
                console.log("Nickname change successful.")
            } 
        } catch (e) {
            console.error("Network error:", e)
        }
    }

    const handleNicknameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setEditedNickname(e.target.value);
    }

    React.useEffect(() => {
        setEditedNickname(nickname);
    }, [nickname]);

    return (
        <MyPageItem
            title={`${t('MyPage:Nickname')}`}
            information={
                <div className="information-wrapper">
                    {!isEditing ? (
                        <p className="information">{editedNickname}</p>
                    ) : (
                        <input
                            type="text"
                            value={editedNickname}
                            onChange={handleNicknameChange}
                            className="nickname-input"
                        />
                    )}
                    {!isEditing && lastModified && (
                        <span className="fs-8 more-information">
                            <p>{t('MyPage:Last_modified')}:</p>
                            <p>{formattedDate}</p>
                        </span>
                    )}
                </div>
            }
            options={
                !isEditing ? (
                    <button onClick={handleEditClick}>
                        <LazyPencilIcon className="desaturated-cyan-icon pencil-icon" />
                    </button>
                ) : (
                    <div className="decision-btns">
                        <button onClick={handleSaveClick}>{t('Common:Save')}</button>
                        <button onClick={handleCancelClick}>{t('Common:Cancel')}</button>
                    </div>
                )
            }
        />
    );
}

export default React.memo(MyPageNickname);