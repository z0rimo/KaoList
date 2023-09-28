import React from "react";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import SongSearchList, { ISongSearchListItem } from "../../SongSearchList";
import Pagination from "../../components/Pagination";
import { useTranslation } from "react-i18next";
import StringHelper from "../../StringHelper";
import LazyStarSolidIcon from "../../svgs/LazyStarSolidIcon";
import LazyStarIcon from "../../svgs/LazyStarIcon";
import "../ChartPage/DiscoverChartPage/DiscoverChartPage.scss";
import "../ChartPage/ChartPage.scss";

const SongSearchListItem = React.memo((props: ISongSearchListItem) => {
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
        <tr className="table-td discover" onClick={navgiateToDetailClick}>
            <td>
                <img alt={StringHelper.format(t('Thumbnail of {0}'), props.title)}
                    src="https://i.ytimg.com/vi/XOxI7bEHQgc/hqdefault.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLC5kqwJDiTRyMg0D5mIsZ0ZyTcvRg" />
            </td>
            <td>
                <p className="title">{props.title}</p>
                <p>{props.songUsers?.map(item => item.nickname).join(", ")}</p>
            </td>
            <td>{tjNo}</td>
            <td>{kumyoungNo}</td>
            <td>
                <button className="like-btn" onClick={() => (setLike(!like))}>
                    {like ? <LazyStarSolidIcon fill="#6BB9A4" /> : <LazyStarIcon fill="#5F6368" />}
                </button>
            </td>
        </tr>
    )
})

const songSearchListItemRender = (item: ISongSearchListItem) => {
    return <SongSearchListItem {...item} />
}

const Table = React.memo((props: React.HTMLAttributes<HTMLTableElement>) => <table {...props} />);

function SearchPage() {
    const { t } = useTranslation('Chart')
    const [totalResults, setTotalResults] = React.useState<number>(0);

    return (
        <MainLayout>
            <MainSection>
                <div className="chart-wrapper discover bottom-right-box-shadow">
                    <SongSearchList
                        maxResults={20}
                        Table={Table}
                        renderer={songSearchListItemRender}
                        setTotalResults={setTotalResults}
                        thead={
                            <thead>
                                <tr>
                                    <tr className="table-th discover">
                                        <th>{t("Thumbnail")}</th>
                                        <th>{t("Title/Artist")}</th>
                                        <th>TJ</th>
                                        <th>KY</th>
                                        <th>{t("Like")}</th>
                                    </tr>
                                </tr>
                            </thead>
                        }
                    />
                </div>
                <Pagination totalResults={totalResults} resultsPerPage={20} />
            </MainSection>
        </MainLayout >
    )
}

export default React.memo(SearchPage);