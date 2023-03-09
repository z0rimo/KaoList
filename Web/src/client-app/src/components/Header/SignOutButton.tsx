import React from "react";
import { useTranslation } from "react-i18next";
import { NavLink, useLocation, } from "react-router-dom";
import { queryParameterNames } from "../identity";

const logoutState = { local: true };

function SignOut() {
    const { t } = useTranslation('Header');
    const location = useLocation();
    const to = React.useMemo(() => {
        return `${window.authPaths.LogOut}?${queryParameterNames.ReturnUrl}=${window.location.origin}${location.pathname}`
    }, [location.pathname])

    return (
        <NavLink className="sign-out fs-8" replace to={to} state={logoutState}>
            {t("Sign Out")}
        </NavLink>
    )
}

export default React.memo(SignOut)