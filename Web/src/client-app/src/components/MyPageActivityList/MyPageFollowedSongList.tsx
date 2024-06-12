import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import Dropdown from "../Dropdown";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";
import { IMyPageFollowedSongResource } from "../../api/models/IMyPageModels";

function MyPageFollowedSongList() {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const [resources, setResources] = React.useState<IMyPageFollowedSongResource[]>([]);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.mypages.myPageFollowedSongList();
            if (response.resources) {
                setResources([...response.resources].take(5));
            }
        })();
    }, []);

    return (
        <Dropdown expanded={display}>
            <MyPageDropdownHead
                title={`${t('Song follow list')}`}
                caseName={`${t('Songs')}`}
                count={resources.length}
                button={
                    <button onClick={handleDisplay}>
                        {display ? <LazyAngleUpIcon className="angle-icon" /> : <LazyAngleDownIcon className="angle-icon" />}
                    </button>
                }
            />
            <>
                {resources?.slice(0,5).map(resource => <MyPageItemWithDate
                    className="dropdown-item"
                    key={resource.id}
                    title={resource.title}
                    date={resource.created !== undefined ? new Date(resource.created) : undefined}
                />)}
            </>
        </Dropdown>
    )
}

export default React.memo(MyPageFollowedSongList);