import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import DateOptionFormatter from "../../components/DateOptionFormatter";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import TableTitle from "../../components/TableTitle";
import LikedChart, { ILikedChartItem } from "../../LikedChart";
import LazyStarSolidIcon from "../../svgs/LazyStarSolidIcon";
import StringHelper from "../../StringHelper";
import LazyArrowUpIcon from "../../svgs/LazyArrowUpIcon";
import LazyArrowDownIcon from "../../svgs/LazyArrowDownIcon";
import LazyStarIcon from "../../svgs/LazyStarIcon";
import LikedChartDropdown from "../../components/LikedChartDropdown";

function RankChange(length: number) {
    if (length === 4) {
        return (
            <>
                <LazyArrowUpIcon className="arrow-icon" fill="#6BB9A4" width="7" height="10" />
                <div className="text-cyan">1</div>
            </>
        )
    }
    else if (length === 9) {
        return (
            <>
                <LazyArrowDownIcon className="arrow-icon" fill="#B96B80" width="7" height="10" />
                <div className="text-pink">1</div>
            </>
        )
    }
    else {
        return (
            <p>-</p>
        )
    }
}

const LikedChartItem = React.memo((props: ILikedChartItem) => {
    const [like, setLike] = useState<boolean>(false);
    const { t } = useTranslation('Chart');
    //const rankChange = RankChange(props.title.length);
    let tjNo = "-";
    let kumyoungNo = "-";

    if (props.karaoke?.providerName === "tj") {
        tjNo = props.karaoke.no ?? "-";
    } else if (props.karaoke?.providerName === "kumyoung") {
        kumyoungNo = props.karaoke.no ?? "-";
    }

    return (
        <tr className="table-td liked">
            <td className="rank">
                <p className="rank-num">1</p>
                <div className="arrow-num-wrapper">
                    {/* {rankChange} */}
                </div>
            </td>
            <td className="thumbnail">
                <img alt={StringHelper.format(t('Thumbnail of {0}'), props.title)}
                    src="https://i.ytimg.com/vi/XOxI7bEHQgc/hqdefault.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLC5kqwJDiTRyMg0D5mIsZ0ZyTcvRg" />
            </td>
            <td className="title-artist">
                <p className="title">{props.title}</p>
                <p><td>{props.songUsers?.map(item => item.nickname).join(", ")}</td></p>
            </td>
            <td>{tjNo}</td>
            <td>{kumyoungNo}</td>
            <td className="like">
                <button className="like-btn" onClick={() => (setLike(!like))}>
                    {like ? <LazyStarSolidIcon width="20" height="20" fill="#6BB9A4" /> : <LazyStarIcon width="20" height="20" fill="#5F6368" />}
                </button>
            </td>
        </tr>
    )
});

const likedCharItemRender: Parameters<typeof LikedChart>[0]['renderer'] = (item) => {
    return <LikedChartItem {...item} />
}

const Table = React.memo((props: React.HTMLAttributes<HTMLTableElement>) => <table {...props} />);

function LikedChartPage() {
    const { t } = useTranslation('Chart');
    const [dropdownState, setDropdownState] = useState('All');
    const periodAry = ["Daily", "Monthly", "All"];
    let now = new Date();

    function dateSettingOption(e: string) {
        if (e === 'Daily') {
            return now.toLocaleDateString(navigator.language, DateOptionFormatter.long)
        }
        else if (e === 'Monthly') {
            return (
                new Date(now.setMonth(now.getMonth() - 1)).toLocaleDateString(navigator.language,  DateOptionFormatter.long)
                + ' ~ '
                +
                new Date().toLocaleDateString(navigator.language,  DateOptionFormatter.long)
            )
        }
        else {
            return (
                ' ~ ' +
                now.toLocaleDateString(navigator.language,  DateOptionFormatter.long)
            )
        }
    }

    const handlePeriodClick = React.useCallback((e: string) => {
        setDropdownState(e);
    }, [])

    return (
        <MainLayout>
            <MainSection>
                <div className="chart-wrapper liked">
                    <TableTitle
                        title={`${t("Liked Chart")}`}
                        date={dateSettingOption(dropdownState)}
                        option={<LikedChartDropdown
                            dropdownState={dropdownState}
                            periodAry={periodAry}
                            handleDropdownClick={handlePeriodClick}
                        />}
                    />
                    <div className="bottom-right-box-shadow">
                        <LikedChart maxResults={50}
                            Table={Table}
                            renderer={likedCharItemRender}
                            thead={
                                <thead>
                                    <tr className="table-th liked">
                                        <th className="rank">{t("Rank")}</th>
                                        <th className="thumbnail">{t("Thumbnail")}</th>
                                        <th className="title-artist">{t("Title/Artist")}</th>
                                        <th className="tj">TJ</th>
                                        <th className="ky">KY</th>
                                        <th className="like">{t("Like")}</th>
                                    </tr>
                                </thead>
                            }
                        />
                    </div>
                </div >
            </MainSection >
        </MainLayout >
    )
}

export default React.memo(LikedChartPage);