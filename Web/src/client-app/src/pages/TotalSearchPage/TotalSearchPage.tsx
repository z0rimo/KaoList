import React from "react";
import './TotalSearchPage.scss';
import MainLayout from "../../layouts/MainLayout";
import MainSection from "../../components/MainSection";
import SongSearchList from "../../SongSearchList";
import SongSearchListItemRenderer from "../../components/SongSearchListItemRenderer";
import { useTranslation } from "react-i18next";
import { useLocation } from "react-router-dom";
import Table from "../../components/Table";
import Pagination from "../../components/Pagination";

function TotalSearchPage(props: React.HtmlHTMLAttributes<HTMLDivElement>) {
  const { t } = useTranslation('Chart')
  const location = useLocation();
  const query = new URLSearchParams(location.search).get('q') || '';
  const [totalResults, setTotalResults] = React.useState<number>(0);

  return (
    <MainLayout>
      <MainSection className="table-section">
        <h1>제목 검색</h1>
        <div className="render-table-wrapper bottom-right-box-shadow">
          <SongSearchList
            maxResults={20}
            Table={Table}
            renderer={(item) => <SongSearchListItemRenderer item={item} q={query} />}
            setTotalResults={setTotalResults}
            thead={
              <thead>
                <tr className="tr-group fs-4">
                  <th>{t("Like")}</th>
                  <th>{t("Thumbnail")}</th>
                  <th>{t("Title/Artist")}</th>
                  <th>{t("Song Number")}</th>
                </tr>
              </thead>
            }
          />
        </div>
        <Pagination className="mt-7 fs-4" totalResults={totalResults} resultsPerPage={20} />
      </MainSection>
    </MainLayout>
  )
}

export default React.memo(TotalSearchPage);