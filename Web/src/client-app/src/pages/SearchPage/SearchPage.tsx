import React from "react";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import SongSearchList, { ISongSearchListItem } from "../../SongSearchList";
import Pagination from "../../components/Pagination";
import { useTranslation } from "react-i18next";
import SongSearchItem from "../../components/SongSearchItem";
import "./SearchPage.scss";

const songSearchListItemRender = (item: ISongSearchListItem) => {
    return <SongSearchItem {...item} />
}

const Table = React.memo((props: React.HTMLAttributes<HTMLTableElement>) => <table {...props} />);

function SearchPage() {
    const { t } = useTranslation('Chart')
    const [totalResults, setTotalResults] = React.useState<number>(0);

    return (
        <MainLayout>
            <MainSection className="table-section">
                <div className="render-table-wrapper bottom-right-box-shadow">
                    <SongSearchList
                        maxResults={20}
                        Table={Table}
                        renderer={songSearchListItemRender}
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