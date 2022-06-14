import React from "react";
import HeaderLogo from "./HeaderLogo";
import HeaderNav from "./HeaderNav";

function HeaderStartRegion() {
    return (
        <div className="header-start-region">
            <HeaderLogo />
            <HeaderNav />
        </div>
    )
}

export default React.memo(HeaderStartRegion);