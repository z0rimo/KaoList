import React from "react";
import { useLocation } from "react-router-dom";
import PathHelper from "../../PathHelper";
import CustomerHeader from "./CustomerHeader";
import MainHeader from "./MainHeader"; 

function Header() {
    let Header: React.ElementType;
    const location = useLocation();

    if (PathHelper.getAreaNameByPath(location.pathname) === 'customer') {
        Header = CustomerHeader;
    }
    else {
        Header = MainHeader;
    }

    return (
        <Header />
    )
}

export default React.memo(Header);
