import React from "react";
import { useTranslation } from "react-i18next";
import NotImplementedError from "../../errors/NotImplementedError";
import ISong from "../../models/ISong";
import StringHelper from "../../StringHelper";
import LazyBarsIcon from "../../svgs/LazyBarsIcon";

const BarsIcon = React.memo(() => {
    return (
        <LazyBarsIcon width="11" height="11" />
    )
});

function PlaylistSong(props: ISong) {
    const { t } = useTranslation('Playlist', { keyPrefix: 'Song' });

    const handleSongDrag = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    return (
        <div className="song-item" onDrag={handleSongDrag}>
            <div className="bars-icon">
                <BarsIcon />
            </div>
            <div className="img-box">
                <img src={props.imgUrl} alt={StringHelper.format(t('Thumbnail of {0}'), props.title)} />
            </div>
            <div className="content-text">
                <strong>{props.title}</strong>
                <p>{props.artist}</p>
            </div>
        </div>
    )
}

export default React.memo(PlaylistSong);
