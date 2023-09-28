import React, { useState } from "react";
import Dropdown from './Dropdown';
import { useTranslation } from "react-i18next";
import LazyAngleUpIcon from "../svgs/LazyAngleUpIcon";
import LazyAngleDownIcon from "../svgs/LazyAngleDownIcon";
import LikedChartDropdownContent from "./LikedChartDropdownContent";

interface ILikedChartDropDownProps extends React.HTMLAttributes<HTMLElement> {
    periodAry: string[];
    dropdownState?: string;
    handleDropdownClick: (filter: string) => void;
}

function LikedChartDropDown(props: ILikedChartDropDownProps) {
    const { t } = useTranslation('Chart');
    const [opened, setOpened] = useState(false);

    return (
        <Dropdown className="drop-down-wrapper">
            <p className="drop-down-content filter">
                {t("Filter")} {opened ? <LazyAngleUpIcon fill="#333333" /> : <LazyAngleDownIcon fill="#333333" />}
            </p>
            <LikedChartDropdownContent
                {...props}
            />
        </Dropdown>
    )
}

export default React.memo(LikedChartDropDown);