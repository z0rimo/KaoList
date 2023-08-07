import React from "react";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import RoutePath from "../../RoutePath";

function FooterMenu() {
    const { t } = useTranslation('Footer');

    return (
        <div className="footer-menu">
            <Link to={RoutePath["terms"]}>{t('Terms of Service')}</Link>
            <Link to={RoutePath["policy"]}>{t('Privacy Policy')}</Link>
            <Link to={RoutePath["inquiry"]}>{t('Inquiry')}</Link>
        </div>
    )
}

export default React.memo(FooterMenu);