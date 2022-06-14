import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import RoutePath from "../../RoutePath";
import LazyHeaderLogoIcon from "../../svgs/LazyHeaderLogoIcon";
import HeaderManageNavPages from "./HeaderManageNavPages";

function HeaderLogo() {
    const location = useLocation();
    const navigate = useNavigate();
    const disabled = HeaderManageNavPages.HomeNavClass(location) !== undefined;

    const handleLogoClick = React.useCallback(() => {
        navigate(RoutePath['home']);
    }, [navigate])

    return (
        <LazyHeaderLogoIcon className={`link ${disabled ? 'disabled' : ''}`} onClick={handleLogoClick} />
    )
}

export default React.memo(HeaderLogo);