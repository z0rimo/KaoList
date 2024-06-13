import React from "react";
import UserProfile from "../UserProfile";
import HeaderEndRegion from "./HeaderEndRegion";
import HeaderLogo from "./HeaderLogo";
import HeaderStartRegion from "./HeaderStartRegion";
import MainHeaderNav from "./MainHeaderNav";
import SongSearchbar from "../../SongSearchbar";
import RoutePath from "../../RoutePath";
import { useLocation } from "react-router-dom";

function MainHeader() {
    const location = useLocation();

    return (
        <header>
            <div className="header-content-region">
                <HeaderStartRegion>
                    <HeaderLogo />
                    <MainHeaderNav />
                </HeaderStartRegion>
                <HeaderEndRegion>
                    {location.pathname !== RoutePath.home && <SongSearchbar className="header-searchbar" />}
                    <UserProfile />
                </HeaderEndRegion>
            </div>
        </header>
    )
}

export default React.memo(MainHeader);