import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import IPlaylist from "../../models/IPlaylist";
import ISong from "../../models/ISong";
import PlaylistSong from "./PlaylistSong";
import LazyCircleMinusIcon from "../../svgs/LazyCircleMinusIcon";
import LazyCirclePlayIcon from "../../svgs/LazyCirclePlayIcon";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import ClassNameHelper from "../../ClassNameHelper";
import PlaylistSharedRole, { PlaylistSharedRoleType } from "../../PlaylistSharedRole";
import LazyLinkSimpleIcon from "../../svgs/LazyLinkSimpleIcon";
import LazyLockKeyholeIcon from "../../svgs/LazyLockKeyholeIcon";
import NotImplementedError from "../../errors/NotImplementedError";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import LazyCaretDownIcon from "../../svgs/LazyCaretDownIcon";

const shareIconObject = {
    [PlaylistSharedRole.unlisted]: React.memo(() => <LazyLinkSimpleIcon width="13" height="9" className="share-role-button" />),
    [PlaylistSharedRole.private]: React.memo(() => <LazyLockKeyholeIcon width="13" height="13" className="share-role-button" />)
};

const ShareIcon = React.memo((props: React.SVGAttributes<SVGSVGElement> & { sharedRole?: PlaylistSharedRoleType }) => {
    const { sharedRole, ...rest } = props;
    const DisplayIcon = shareIconObject[sharedRole ?? PlaylistSharedRole.private];
    return <DisplayIcon {...rest} />
});

const RemoveButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
    return (
        <button className="btn" {...props}>
            <LazyCircleMinusIcon className="circle-minus-icon" width="15" height="15" />
        </button>
    )
});

const PlayButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
    return (
        <button className="btn" {...props}>
            <LazyCirclePlayIcon className="play-icon" width="24" height="24" />
        </button>
    )
});

const SharedRoleButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement> & { sharedRole?: PlaylistSharedRoleType }) => {
    return (
        <button className="btn" {...props}>
            <ShareIcon sharedRole={props.sharedRole} />
            <LazyCaretDownIcon className="caret-down-icon" width="6" height="4" />
        </button>
    )
});

const SharedAddressButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
    const { t } = useTranslation('Playlist', { keyPrefix: 'List' });

    return (
        <button className="list-button" {...props}>
            {t('Share')}
        </button>
    )
});

const OpenButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement> & { opened?: boolean }) => {
    const { opened, ...rest } = props;
    return (
        <button className="btn" {...rest}>
            {opened ? <LazyAngleUpIcon /> : <LazyAngleDownIcon />}
        </button>
    )
});

const dateOptions: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false
};

const PlaylistSongCollection = React.memo((props: { songs: ISong[] }) => {
    return (
        <div>
            {props?.songs.map(item => <PlaylistSong key={item.id} {...item} />)}
        </div>
    )
});

function Playlist(props: IPlaylist) {
    const [opened, setOpened] = useState(false);

    const handlePlaylistDrag = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    const handleRemoveButtonClick = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    const handlePlayButtonClick = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    const handleSharedRoleButtonClick = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    const handleOpenClick = React.useCallback(() => {
        setOpened(!opened);
    }, [opened]);

    return (
        <div className="list" onDrag={handlePlaylistDrag}>
            <ul className={ClassNameHelper.concat("list-content", opened && "active")}>
                <li className="left">
                    <RemoveButton onClick={handleRemoveButtonClick} />
                    <p>{props.title}</p>
                    <PlayButton onClick={handlePlayButtonClick} />
                    <SharedRoleButton onClick={handleSharedRoleButtonClick} />
                </li>                
                <li className="right">
                    <p>{props.viewCount} | {props.sharedCount} | {props.createdTime.toLocaleString(navigator.language, dateOptions)}</p>
                    <SharedAddressButton />
                    <OpenButton onClick={handleOpenClick} opened={opened} />
                </li>
            </ul>
            {opened && <PlaylistSongCollection songs={props.songs} />}
        </div>
    )
}

export default React.memo(Playlist);