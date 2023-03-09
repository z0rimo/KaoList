import React from "react";
import { useTranslation } from 'react-i18next';
import { useNavigate } from "react-router-dom";
import { I18nResourcesKeyType } from "../../i18n/I18nResources";
import HeaderStartRegion from "./HeaderStartRegion";
import SignOutButton from "./SignOutButton";

function Header() {
    const { t } = useTranslation<I18nResourcesKeyType>('Header');
    const navigate = useNavigate();

    const handleSignInClick = React.useCallback(() => {
        navigate(window.authPaths.Login);
    }, [navigate]);

    const handleSignOutClick = React.useCallback(() => {
        navigate(window.authPaths.LogOut + '?returnUrl=/', {
            state: {local: true}
        });
    }, [navigate]);

    return (
        <header>
            <div className="content-region">
                <HeaderStartRegion />
                <div>
                    <button onClick={handleSignInClick}>{t('SignIn')}</button>

                </div>
                <SignOutButton />
            </div>
        </header>
    );
}

export default React.memo(Header);
