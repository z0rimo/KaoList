import React from "react";
import LazyStarSolidIcon from "../svgs/LazyStarSolidIcon";
import LazyStarIcon from "../svgs/LazyStarIcon";
import { ISongSearchListItem } from "../SongSearchList";
import { useTranslation } from "react-i18next";
import StringHelper from "../StringHelper";

function SongSearchItem(props: ISongSearchListItem) {
  const [like, setLike] = React.useState<boolean>(false);
  const { t } = useTranslation('Chart')
  let tjNo = "-";
  let kumyoungNo = "-";

  if (props.karaoke?.providerName === "tj") {
      tjNo = props.karaoke.no ?? "-";
  } else if (props.karaoke?.providerName === "kumyoung") {
      kumyoungNo = props.karaoke.no ?? "-";
  }

  const navgiateToDetailClick = () => {
      window.location.href = `/songs/detail?id=${props.id}`;
  }

  return (
    <tr className="tr-group fs-4">
          <td className="center-layout">
              <img alt={StringHelper.format(t('Thumbnail of {0}'), props.title)}
                  src="https://i.ytimg.com/vi/XOxI7bEHQgc/hqdefault.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLC5kqwJDiTRyMg0D5mIsZ0ZyTcvRg" />
          </td>
          <td>
              <p className="fw-bold">{props.title}</p>
              <p>{props.songUsers?.map(item => item.nickname).join(", ")}</p>
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