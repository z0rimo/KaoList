import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import LazyCircleMinusIcon from "../../svgs/LazyCircleMinusIcon";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";

interface IMyPageAccessHistoryItem {
    information?: string;
    date?: Date;
}

interface IMyPageAccessHistoryProps {
    count?: number;
    items?: IMyPageAccessHistoryItem[]
}

const defaultItems: IMyPageAccessHistoryItem[] = [
    { information: "Korea", date: new Date(2007, 1, 28, 11, 39, 7) },
    { information: "Japan", date: new Date(2007, 1, 29, 11, 39, 7) },
]

function MyPageAccessHistory({ items = defaultItems, count = 2 }: IMyPageAccessHistoryProps) {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    return (
        <>
            <MyPageDropdownHead
                title={`${t('Access history')}`}
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

export default React.memo(MyPageAccessHistory);