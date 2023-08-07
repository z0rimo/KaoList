import React from "react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../../ClassNameHelper";
import RoutePath from "../../RoutePath";
import HeaderManageNavPages from "./HeaderManageNavPages";

function CustomerHeaderNav() {
    const { t } = useTranslation('Footer');
    const location = useLocation();
    const navigate = useNavigate();
    
    const handleClick = React.useCallback((path: string) => {
        navigate(path);
    }, [navigate]);

    const handleTermsClick = React.useCallback(() => {
        handleClick(RoutePath['terms']);
    }, [handleClick]);

    const handlePolicyClick = React.useCallback(() => {
        handleClick(RoutePath['policy']);
    }, [handleClick]);

    const handleInquiryClick = React.useCallback(() => {
        handleClick(RoutePath['inquiry']);
    }, [handleClick]);

    return (
        <nav className="nav">
            <ul className="main-nav customer">
                <li className={ClassNameHelper.concat(HeaderManageNavPages.TermsNavClass(location), 'link')}
                    onClick={handleTermsClick}>{t('Terms of Service')}</li>
                <li className={ClassNameHelper.concat(HeaderManageNavPages.PolicyNavClass(location), 'link')}
                    onClick={handlePolicyClick}>{t('Privacy Policy')}</li>
                <li className={ClassNameHelper.concat(HeaderManageNavPages.InquiryNavClass(location), 'link')}
                    onClick={handleInquiryClick}>{t('Inquiry')}</li>
            </ul>
        </nav>
    )
}

export default React.memo(CustomerHeaderNav);