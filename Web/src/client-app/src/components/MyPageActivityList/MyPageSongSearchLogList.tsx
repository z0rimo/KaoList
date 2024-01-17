import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import LazyCircleMinusIcon from "../../svgs/LazyCircleMinusIcon";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";
import { IMyPageSongSearchLogResource } from "../../api/kaolistApi";
import Dropdown from "../Dropdown";

function MyPageSongSearchLogList() {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const [resources, setResources] = React.useState<IMyPageSongSearchLogResource[]>([]);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.mypages.myPageSongSearchLogList();
            if (response.resources) {
                setResources([...response.resources]);
            }
        })();
    }, []);

    return (
        <Dropdown expanded={display}>
            <MyPageDropdownHead
                title={`${t('Recent song search history')}`}
                caseName={`${t('History')}`}
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
                    information={
                        <p>
                            {resource.query}
                        </p>
                    }
                    date={resource.created !== undefined ? new Date(resource.created) : undefined}
                    options={
                        <button>
                            <LazyCircleMinusIcon className="desaturated-pink-icon circle-minus-icon" />
                        </button>
                    }
                />)}
            </>
        </Dropdown>
    )
}

export default React.memo(MyPageSongSearchLogList);