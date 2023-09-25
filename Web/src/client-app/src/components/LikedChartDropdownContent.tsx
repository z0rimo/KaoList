import React from "react";
import { useTranslation } from "react-i18next";
import LazyCheckedIcon from "../svgs/LazyCheckedIcon";
import ClassNameHelper from "../ClassNameHelper";
import { useNavigate } from "react-router-dom";

interface ILikedChartDropDownProps extends React.HTMLAttributes<HTMLElement> {
    periodAry: string[];
    dropdownState?: string;
    handleDropdownClick: (filter: string) => void;
}

function LikedChartDropDownContent(props: ILikedChartDropDownProps) {
    const { t } = useTranslation('Chart');
    const navigate = useNavigate();

    const handleClick = (filter: string) => {
        props.handleDropdownClick(filter);
        if (filter === 'All') {
            navigate('/chart/likeTotal?page=1');
        } else {
            navigate('/chart/like?page=1');
        }
    };

    return (
        <ul>
            <li className="drop-down-content period" >{t("Period")}</li>
            {props.periodAry.map((filter) => {
                return (
                    <li
                        key={filter} className="drop-down-content item"
                        onClick={() => handleClick(filter)} >
                        {`${t(filter)}`}
                        <LazyCheckedIcon fill="transparent" className={ClassNameHelper.concat('checked-icon',
                            props?.dropdownState === filter && "active")} />
                    </li>)
            })}
        </ul>
    )
}

export default React.memo(LikedChartDropDownContent);