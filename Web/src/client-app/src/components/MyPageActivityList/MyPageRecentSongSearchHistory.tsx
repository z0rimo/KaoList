import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import LazyCircleMinusIcon from "../../svgs/LazyCircleMinusIcon";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";

interface IMyPageRecentSongSearchHistoryItem {
    information?: string;
    date?: Date;
}

interface IMyPageRecentSongSearchHistoryProps {
    count?: number;
    items?: IMyPageRecentSongSearchHistoryItem[]
}

const defaultItems: IMyPageRecentSongSearchHistoryItem[] = [
    { information: "소나기", date: new Date(2007, 1, 28, 11, 39, 7) },
    { information: "Over my head", date: new Date(2007, 1, 29, 11, 39, 7) },
]

function MyPageRecentSongSearchHistory({ items = defaultItems, count = 2 }: IMyPageRecentSongSearchHistoryProps) {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    return (
        <>
            <MyPageDropdownHead
                title={`${t('Recent song search history')}`}
                caseName={`${t('History')}`}
                count={count}
                button={
                    <button onClick={handleDisplay}>
                        {display ? <LazyAngleUpIcon className="angle-icon" /> : <LazyAngleDownIcon className="angle-icon" />}
                    </button>
                }
            />
            {display &&
                <>
                    {items?.map(item => <MyPageItemWithDate
                        className="dropdown-item"
                        key={item.date?.toLocaleDateString()}
                        information={
                            <p>
                                {item.information}
                            </p>
                        }
                        date={item.date}
                        options={
                            <button>
                                <LazyCircleMinusIcon className="desaturated-pink-icon circle-minus-icon" />
                            </button>
                        }
                    />)}
                </>
            }
        </>
    )
}

export default React.memo(MyPageRecentSongSearchHistory);