import React, { useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { GenerateUUID } from "../../../components/GenerateUUID";
import RoutePath from "../../../RoutePath";
import MainLayout from "../../../layouts/MainLayout";
import MainSection from "../../../components/MainSection";
import './NotFoundPage.scss';
import { useTranslation } from "react-i18next";
import kaoListApi from "../../../api/kaolistApi";

function NotFoundPage(props: React.HtmlHTMLAttributes<HTMLDivElement>) {
  const { t } = useTranslation('Common');
  const uuid = GenerateUUID();
  const location = useLocation();
  const currentUrl = location.pathname + location.search;

  useEffect(() => {
    const logMessage = `404 Not Found - URL: ${currentUrl}`;
    kaoListApi.logs.sendAppLog(logMessage).catch(console.error);
  }, [currentUrl]);


  const historyBackClick = (evt: React.MouseEvent<HTMLAnchorElement>) => {
    evt.preventDefault();
    window.history.back();
  };

  return (
    <MainLayout>
      <MainSection>
        <div className="mt-8">
          <p className="error-code">404 NOT FOUND</p>
          <p className="error-title mb-4">{t('Error')}</p>
          <p className="mb-3">not_found_exception</p>
          <p className="uuid mb-5">Error ID : {uuid}</p>
          <div className="link-wrapper">
            <Link to={RoutePath['home']}>HOME</Link>
            <Link to="#" onClick={historyBackClick}>PREVIOUS PAGE</Link>
          </div>
        </div>
      </MainSection>
    </MainLayout>
  )
}

export default React.memo(NotFoundPage);