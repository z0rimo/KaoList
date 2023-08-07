import React from "react";
import UserProfile from "../UserProfile";
import HeaderEndRegion from "./HeaderEndRegion";
import HeaderLogo from "./HeaderLogo";
import HeaderStartRegion from "./HeaderStartRegion";
import MainHeaderNav from "./MainHeaderNav";

function MainHeader() {
    return (
        <header>
            <div className="header-content-region">
                <HeaderStartRegion>
                    <HeaderLogo />
                    <MainHeaderNav />
                </HeaderStartRegion>
                <HeaderEndRegion>
                    <UserProfile />
                </HeaderEndRegion>
            </div>
        </header>
    )
}

export default React.memo(MainHeader);