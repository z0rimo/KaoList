import React from "react";
import { useTranslation } from "react-i18next";
import { I18nResourcesKeyType } from "../../i18n/I18nResources";
import Dropdown from "../Dropdown";
import ChartDropDownContent from "./ChartDropDownContent";

function ChartDropDown() {
    const { t } = useTranslation<I18nResourcesKeyType>('Header');

    return (
        <Dropdown className="chart-dropdown" activeAction="hover">
            <p>{t('Chart')}</p>
            <ChartDropDownContent className="chart-nav bottom-right-box-shadow" />
        </Dropdown>
    )
}

export default React.memo(ChartDropDown);