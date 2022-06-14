import React, { MouseEvent } from "react";
import { useTranslation } from "react-i18next";
import { Location, useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../../ClassNameHelper";
import { I18nResourcesKeyType } from "../../i18n/I18nResources";
import RoutePath from "../../RoutePath";

function disabledByPath(location: Location, path: string) {
    return location.pathname === path && "disabled";
}

function ChartDropDownContent(props: React.HTMLAttributes<HTMLElement>) {
    const { t } = useTranslation<I18nResourcesKeyType>('Header');
    const location = useLocation();
    const navigate = useNavigate();

    const handleClick = React.useCallback((evt: MouseEvent, path: string) => {
        navigate(path);
    }, [navigate]);

    const handleDiscoverChartClick = React.useCallback((evt: MouseEvent) => {
        handleClick(evt, RoutePath['discoverChart']);
    }, [handleClick]);

    const handleLikedChartClick = React.useCallback((evt: MouseEvent) => {
        handleClick(evt, RoutePath['likedChart']);
    }, [handleClick]);


    return (
        <ul {...props}>
            <li className={ClassNameHelper.concat(
                "link",
                disabledByPath(location, RoutePath['discoverChart'])
            )} onClick={handleDiscoverChartClick} >{t('Discover Chart')}</li>
            <li className={ClassNameHelper.concat(
                "link",
                disabledByPath(location, RoutePath['likedChart'])
            )} onClick={handleLikedChartClick}>{t('Liked Chart')}</li>
        </ul>
    )
}

export default React.memo(ChartDropDownContent);