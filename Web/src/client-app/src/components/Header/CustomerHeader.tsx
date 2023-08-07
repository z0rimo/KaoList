import React from "react";
import UserProfile from "../UserProfile";
import CustomerHeaderNav from "./CustomerHeaderNav";
import HeaderEndRegion from "./HeaderEndRegion";
import HeaderLogo from "./HeaderLogo";
import HeaderStartRegion from "./HeaderStartRegion";

function CustomerHeader() {
    return (
        <header className="lime-theme">
            <div className="header-content-region">
                <HeaderStartRegion>
                    <HeaderLogo />
                    <CustomerHeaderNav />
                </HeaderStartRegion>
                <HeaderEndRegion>
                    <UserProfile />
                </HeaderEndRegion>
            </div>
        </header>
    );
}

export default React.memo(CustomerHeader);