import React from "react";
import LazyStarSolidIcon from "../svgs/LazyStarSolidIcon";
import LazyStarIcon from "../svgs/LazyStarIcon";
import { ISongSearchListItem } from "../SongSearchList";
import HighlightMatch from "./HighlightMatch";
import { useLocation, useNavigate } from "react-router-dom";
import { queryParameterNames } from "./identity";
import { useIdentityContext } from "../contexts/IdentityContext";
import LazyNoImageIcon from "../svgs/LazyNoImageIcon";

interface ISongSearchItemProps {
    item: ISongSearchListItem;
    q?: string
}

function SongSearchItem({ item, q }: ISongSearchItemProps) {
    const identityContext = useIdentityContext();
    const navigate = useNavigate();
    const location = useLocation();
    const [like, setLike] = React.useState<boolean>(false);
    const isLoggedIn = identityContext.user ? true : false;

    let tjNo = "-";
    let kumyoungNo = "-";
    let joysoundNo = "-";
    let damNo = "-";
    let ugaNo = "-";

    if (item.karaoke?.providerName === "tj") {
        tjNo = item.karaoke.no ?? "-";
    } else if (item.karaoke?.providerName === "kumyoung") {
        kumyoungNo = item.karaoke.no ?? "-";
    } else if (item.karaoke?.providerName === "joysound") {
        joysoundNo = item.karaoke.no ?? "-";
    } else if (item.karaoke?.providerName === "dam") {
        damNo = item.karaoke.no ?? "-";
    } else if (item.karaoke?.providerName === "uga") {
        ugaNo = item.karaoke.no ?? "-";
    }

    const handleLikeClick = (e: React.MouseEvent<HTMLTableCellElement>) => {
        e.stopPropagation();
        if (!isLoggedIn) {
            alert("좋아요 기능은 로그인을 하셔야합니다.");
            navigate(`${window.authPaths.Login}?${queryParameterNames.ReturnUrl}=${encodeURIComponent(window.location.origin + location.pathname + `?q=${q}`)}`);
            return;
        }
        setLike(!like);
    };

    return (
        <tr className="tr-group fs-4">
            <td className="td-1" onClick={handleLikeClick} style={{ cursor: 'pointer' }}>
                {like ? <LazyStarSolidIcon fill="#6BB9A4" /> : <LazyStarIcon fill="#5F6368" />}
            </td>
            <td className="td-2">
                <LazyNoImageIcon className="thumbnail-icon" style={{ width: 'var(--thumbnail-width)', height: 'var(--thumbnail-height)', backgroundColor: "#d9d9d9" }} />
            </td>
            <td className="td-3">
                <p className="fw-bold">
                    <HighlightMatch text={item.title} query={q} />
                </p>
                <p>
                    {item.songUsers?.map((user, index) => (
                        <React.Fragment key={index}>
                            {index > 0 ? ", " : ""}
                            <HighlightMatch text={user.nickname} query={q} />
                        </React.Fragment>
                    ))}
                </p>
            </td>
            <td className="td-4">
                <div className="center-layout">
                    <div className="no-box-container">
                        <div className="no-box-wrapper">
                            <div className="no-box tj fs-11">TJ</div>
                            <div className="fs-8">{tjNo}</div>
                        </div>
                        <div className="no-box-wrapper">
                            <div className="no-box fs-11">KY</div>
                            <div className="fs-8">{kumyoungNo}</div>
                        </div>
                    </div>
                    <div className="no-box-container">
                        <div className="no-box-wrapper">
                            <div className="no-box fs-11">Joysound</div>
                            <div className="fs-8">{joysoundNo}</div>
                        </div>
                        <div className="no-box-wrapper">
                            <div className="no-box fs-11">DAM</div>
                            <div className="fs-8">{damNo}</div>
                        </div>
                        <div className="no-box-wrapper">
                            <div className="no-box uga fs-11">UGA</div>
                            <div className="fs-8">{ugaNo}</div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    )
}

export default React.memo(SongSearchItem);