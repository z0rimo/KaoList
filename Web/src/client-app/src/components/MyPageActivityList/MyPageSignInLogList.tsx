import React from "react";
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../../svgs/LazyAngleUpIcon";
import MyPageItemWithDate from "../mypage/MyPageItemWithDate";
import LazyAngleDownIcon from "../../svgs/LazyAngleDownIcon";
import LazyCircleMinusIcon from "../../svgs/LazyCircleMinusIcon";
import MyPageDropdownHead from "../mypage/MyPageDropdownHead";
import { IMyPageSignInLogResource } from "../../api/kaolistApi";
import Dropdown from "../Dropdown";

function MyPageSignInLogList() {
    const { t } = useTranslation("MyPage");
    const [display, setDisplay] = React.useState(false);
    const [resources, setResources] = React.useState<IMyPageSignInLogResource[]>([]);
    const handleDisplay = React.useCallback(() => {
        setDisplay(!display);
    }, [display]);

    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.mypages.myPageSignInLogList();
            if (response.resources) {
                setResources([...response.resources]);
            }
        })();
    }, []);

    return (
        <Dropdown expanded={display}>
            <MyPageDropdownHead
                title={`${t('Access history')}`}
                caseName={`${t('History')}`}
                count={resources.length}
                button={
                    <button onClick={handleDisplay}>
                        {display ? <LazyAngleUpIcon className="angle-icon" /> : <LazyAngleDownIcon className="angle-icon" />}
                    </button>
                }
            />
            <div>
                {display &&
                    <>
                        {resources?.slice(0,5).map(resource => <MyPageItemWithDate
                            className="dropdown-item"
                            key={resource.id}
                            information={
                                <p>
                                    {resource.ipAddress}
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
                }
            </div>
        </Dropdown>
    )
}

export default React.memo(MyPageSignInLogList);