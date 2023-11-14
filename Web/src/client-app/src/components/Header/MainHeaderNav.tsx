import React from "react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../../ClassNameHelper";
import { useIdentityContext } from "../../contexts/IdentityContext";
import RoutePath from "../../RoutePath";
import ChartDropDown from "./ChartDropDown";
import HeaderManageNavPages from "../HeaderManageNavPages";

function MainHeaderNav() {
    const { t } = useTranslation('Header');
    const location = useLocation();
    const navigate = useNavigate();
    const { user } = useIdentityContext();

    const handleClick = React.useCallback((path: string) => {
        navigate(path);
    }, [navigate]);

    const handleHomeClick = React.useCallback(() => {
        handleClick(RoutePath['home']);
    }, [handleClick]);

    const handlePlaylistClick = React.useCallback(() => {
        handleClick(RoutePath['playlist']);
    }, [handleClick]);

    return (
        <nav className="nav">
            <ul className="main-nav">
                <li className={ClassNameHelper.concat(HeaderManageNavPages.HomeNavClass(location), 'link')}
                    onClick={handleHomeClick}>{t('Home')}</li>
                <li className={HeaderManageNavPages.ChartNavClass(location)}>
                    <ChartDropDown />
                </li>
                {user &&
                    <li className={ClassNameHelper.concat(HeaderManageNavPages.PlaylistNavClass(location), 'link')}
                        onClick={handlePlaylistClick}>{t('Playlist')}
                    </li>
                }
            </ul>
        </nav>
    )
}

export default React.memo(MainHeaderNav);