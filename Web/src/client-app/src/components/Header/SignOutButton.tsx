import React from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

function SignOutButton() {
    const { t } = useTranslation('Header');
    const navigate = useNavigate();

    const handleSignOutClick = React.useCallback(() => {
        navigate(window.authPaths.LogOut + '?returnUrl=/', {
            state: {local: true}
        });
    }, [navigate]);

    return (
        <a className="sign-out" onClick={handleSignOutClick}>{t('Sign Out')}</a>
    )
}

export default React.memo(SignOutButton)
