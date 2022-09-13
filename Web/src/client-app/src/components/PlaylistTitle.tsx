import React from "react";
import { useTranslation } from "react-i18next";
import RoutePath from "../RoutePath";
import LazyPlaylistTitleIcon from "../svgs/LazyPlaylistTitleIcon";

function PlaylistTitle() {
    const { t } = useTranslation('PlaylistTitle');

    return (
        <a href={RoutePath['playlist']} className="title">
            <LazyPlaylistTitleIcon className="title-list-icon" />
            {t("Playlist")}
        </a>
    )
}

export default React.memo(PlaylistTitle);