import React from "react";
import { useTranslation } from "react-i18next";
import LazyCheckedIcon from "../svgs/LazyCheckedIcon";
import ClassNameHelper from "../ClassNameHelper";

interface ILikedChartDropDownProps extends React.HTMLAttributes<HTMLElement> {
    periodAry: string[];
    dropdownState?: string;
    handleDropdownClick: (filter: string) => void;
}

function LikedChartDropDownContent(props: ILikedChartDropDownProps) {
    const { t } = useTranslation('Chart');

    return (
        <ul {...props}>
            <li className="drop-down-content period" >{t("Period")}</li>
            {props.periodAry.map((filter) => {
                return (
                    <li
                        key={filter} className="drop-down-content item"
                        onClick={() => props.handleDropdownClick(filter)} >
                        {`${t(filter)}`}
                        <LazyCheckedIcon fill="transparent" className={ClassNameHelper.concat('checked-icon',
                            props?.dropdownState === filter && "active")} />
                    </li>)
            })}
        </ul>
    )
}

export default React.memo(LikedChartDropDownContent);