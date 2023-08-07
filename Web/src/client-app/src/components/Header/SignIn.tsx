import React from "react";
import { useTranslation } from "react-i18next";
import { NavLink, useLocation } from "react-router-dom";
import { queryParameterNames } from "../identity";

const loginState = { local: true };

function SignIn() {
    const { t } = useTranslation('Header');
    const location = useLocation();
    const to = React.useMemo(() => {
        return `${window.authPaths.Login}?${queryParameterNames.ReturnUrl}=${window.location.origin}${location.pathname}`
    }, [location.pathname])

    return (
        <NavLink className="btn btn-primary fs-8 sign-in" replace to={to} state={loginState}>
            {t('Sign In')}
        </NavLink>
    )
}

export default React.memo(SignIn)