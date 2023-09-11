import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import Dropdown from "../Dropdown";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";

interface IMyPageSongFollowListItem {
    title?: string;
    date?: Date;
}

interface IMyPageSongFollowListProps {
    count?: number;
    items?: IMyPageSongFollowListItem[]
}

const defaultItems: IMyPageSongFollowListItem[] = [
    { title: "소나기", date: new Date(2007, 1, 28, 11, 39, 7) },
    { title: "ENDLESS RAIN", date: new Date(2007, 1, 29, 11, 39, 7) },
]

function MyPageSongFollowList({ items = defaultItems, count = 1223 }: IMyPageSongFollowListProps) {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    const [itemsd, setItemsd] = React.useState<IMyPageSongFollowListItem[]>([]);

    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.mypages.myPageFollowedSongList();

            if (response.resources) {
                setItemsd(response.resources.map(item => {
                    
                }))
            }
        })
    })

    return (
        <Dropdown>
            <MyPageDropdownHead
                title={`${t('Song follow list')}`}
                caseName={`${t('Songs')}`}
                count={count}
                button={
                    <button onClick={handleDisplay}>
                        {display ? <LazyAngleUpIcon className="angle-icon" /> : <LazyAngleDownIcon className="angle-icon" />}
                    </button>
                }
            />
            <div>
                {display &&
                    <>
                        {items?.map(item => <MyPageItemWithDate
                            className="dropdown-item"
                            key={item.date?.toLocaleDateString()}
                            title={item.title}
                            date={item.date}
                        />)}
                    </>
                }
            </div>
        </Dropdown>
    )
}

export default React.memo(MyPageSongFollowList);