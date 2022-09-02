import React from "react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import { queryParameterNames } from "../identity";


function SignInButton() {
    const { t } = useTranslation('Header');
    const navigate = useNavigate();
    const location = useLocation();

    const handleSignInClick = React.useCallback(() => {
        let returnUrl = `?${queryParameterNames.ReturnUrl}=${window.location.href}`;
        navigate(window.authPaths.Login + returnUrl);
    }, [navigate, location]);

    return (
        <button className="sign-in" onClick={handleSignInClick}>
            {t('Sign In')}
        </button>
    )
}

export default React.memo(SignInButton)