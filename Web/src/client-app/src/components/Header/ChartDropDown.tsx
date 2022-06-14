import React from "react";
import { useTranslation } from "react-i18next";
import { I18nResourcesKeyType } from "../../i18n/I18nResources";
import Dropdown from "../Dropdown";
import ChartDropDownContent from "./ChartDropDownContent";

function ChartDropDown() {
    const { t } = useTranslation<I18nResourcesKeyType>('Header');

    return (
        <Dropdown className="chart-dropdown" >
            <Dropdown.Header>
                <p>{t('Chart')}</p>
            </Dropdown.Header>
            <Dropdown.Content>
                <ChartDropDownContent className="chart-nav" />
            </Dropdown.Content>
        </Dropdown>
    )
}

export default React.memo(ChartDropDown);