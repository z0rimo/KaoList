import React from "react";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import SongSearchList from "../../SongSearchList";
import Pagination from "../../components/Pagination";
import { useTranslation } from "react-i18next";
import "./SearchPage.scss";
import Table from "../../components/Table";
import SongSearchListItemRenderer from "../../components/SongSearchListItemRenderer";
import { useLocation } from "react-router-dom";

function SearchPage() {
    const { t } = useTranslation('Chart')
    const location = useLocation();
    const query = new URLSearchParams(location.search).get('q') || undefined;
    const [totalResults, setTotalResults] = React.useState<number>(0);

    return (
        <MainLayout>
            <MainSection className="table-section">
                <div className="render-table-wrapper bottom-right-box-shadow">
                    <SongSearchList
                        maxResults={20}
                        Table={Table}
                        renderer={(item) => <SongSearchListItemRenderer item={item} q={query} />}
                        setTotalResults={setTotalResults}
                        thead={
                            <thead>
                                <tr className="tr-group fs-4">
                                    <th>{t("Thumbnail")}</th>
                                    <th>{t("Title/Artist")}</th>
                                    <th>TJ</th>
                                    <th>KY</th>
                                    <th>{t("Like")}</th>
                                </tr>
                            </thead>
                        }
                    />
                </div>
                <Pagination className="mt-7 fs-4" totalResults={totalResults} resultsPerPage={20} />
            </MainSection>
        </MainLayout >
    )
}

export default React.memo(SearchPage);