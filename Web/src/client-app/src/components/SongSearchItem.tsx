import React from "react";
import LazyStarSolidIcon from "../svgs/LazyStarSolidIcon";
import LazyStarIcon from "../svgs/LazyStarIcon";
import { ISongSearchListItem } from "../SongSearchList";
import { useTranslation } from "react-i18next";
import StringHelper from "../StringHelper";
import HighlightMatch from "./HighlightMatch";

interface ISongSearchItemProps {
    item: ISongSearchListItem;
    q?: string
}

function SongSearchItem({ item, q }: ISongSearchItemProps) {
    const [like, setLike] = React.useState<boolean>(false);
    const { t } = useTranslation('Chart')
    let tjNo = "-";
    let kumyoungNo = "-";

    if (item.karaoke?.providerName === "tj") {
        tjNo = item.karaoke.no ?? "-";
    } else if (item.karaoke?.providerName === "kumyoung") {
        kumyoungNo = item.karaoke.no ?? "-";
    }

    const navgiateToDetailClick = () => {
        window.location.href = `/songs/detail?id=${item.id}`;
    }

    return (
        <tr className="tr-group fs-4">
            <td className="center-layout">
                <img alt={StringHelper.format(t('Thumbnail of {0}'), item.title)}
                    src="https://i.ytimg.com/vi/XOxI7bEHQgc/hqdefault.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLC5kqwJDiTRyMg0D5mIsZ0ZyTcvRg" />
            </td>
            <td>
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
            <td>{tjNo}</td>
            <td>{kumyoungNo}</td>
            <td>
                {like ? <LazyStarSolidIcon fill="#6BB9A4" /> : <LazyStarIcon fill="#5F6368" />}
            </td>
        </tr>
    )
}

export default React.memo(SongSearchItem);