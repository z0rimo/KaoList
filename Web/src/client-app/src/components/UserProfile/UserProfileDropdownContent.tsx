import React from "react";
import { useTranslation } from "react-i18next";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import LazyGearIcon from "../../svgs/LazyGearIcon";
import LazyLogo from "../../svgs/LazyLogo";
import SignOut from "../Header/SignOut";

interface IUserProfileDropdownContentProps extends React.HTMLAttributes<HTMLDivElement> {
    name?: string;
    nickname?: string;
    onClick?: React.MouseEventHandler<HTMLDivElement>;
}

function UserProfileDropdownContent(props: IUserProfileDropdownContentProps) {
    const { name, nickname, onClick, ...rest } = props;
    const { t } = useTranslation("UserProfileDropdown");

    return (
        <div {...rest}>
            <div className="dropdown-content-upper-region">
                <LazyLogo className="dropdown-logo" />
                <SignOut />
            </div>
            <div className="dropdown-content-lower-region">
                <div className="user-image-wrapper">
                    <LazyCircleProfileIcon />
                    <div className="rounded-circle user-image-change" onClick={onClick}>
                        <LazyGearIcon className="gear-icon" />
                    </div>
                </div>
                <div className="account-info">
                    <div className="nickname fs-4">
                        {nickname}
                    </div>
                    <div className="email fs-8">
                        {name}
                    </div>
                    <div className="account-settings fs-9" onClick={onClick}>
                        {t('Account Settings')}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default React.memo(UserProfileDropdownContent);
