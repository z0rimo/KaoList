import React, { MouseEvent, MouseEventHandler } from "react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../../ClassNameHelper";
import { I18nResourcesKeyType } from "../../i18n/I18nResources";
import RoutePath from "../../RoutePath";
import ChartDropDown from "./ChartDropDown";
import HeaderManageNavPages from "./HeaderManageNavPages";
import { Profile } from "oidc-client";
import authService from "../../api-authorization/AuthorizeService";

function HeaderNav() {
    const { t } = useTranslation<I18nResourcesKeyType>('Header');
    const location = useLocation();
    const navigate = useNavigate();
    const [user, setUser] = React.useState<Profile | null>(null);

    const handleClick = React.useCallback((evt: MouseEvent, path: string) => {
        navigate(path);
    }, [navigate]);

    const handleHomeClick = React.useCallback((evt: MouseEvent) => {
        handleClick(evt, RoutePath['home']);
    }, [handleClick]);

    const handleCommunityClick = React.useCallback<MouseEventHandler>((evt: MouseEvent) => {
        handleClick(evt, RoutePath['community']);
    }, [handleClick]);

    const handlePlaylistClick = React.useCallback<MouseEventHandler>((evt: MouseEvent) => {
        handleClick(evt, RoutePath['playlist']);
    }, [handleClick]);

    React.useEffect(() => {
        let isCancelled = false;
        const fetchUserAsync = async () => {
            if (!isCancelled) {
                setUser(await authService.getUser());
            }
        };
        
        fetchUserAsync();
 
        return () => {
            isCancelled = true;
        }
    }, []);

    return (
        <nav className="nav">
            <ul className="main-nav">
                <li className={ClassNameHelper.concat(HeaderManageNavPages.HomeNavClass(location), 'link')}
                    onClick={handleHomeClick}>{t('Home')}</li>
                <li className={HeaderManageNavPages.ChartNavClass(location)}>
                    <ChartDropDown />
                </li>
                <li className={ClassNameHelper.concat(HeaderManageNavPages.CommunityNavClass(location), 'link')}
                    onClick={handleCommunityClick}>{t('Community')}
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

export default React.memo(HeaderNav);