import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { useSearchContext } from "../../contexts/SearchContext";
import DiscoverChartItem from "../../components/DiscoverChartItem";
import LikedChartItem from "../../components/LikedChartItem";
import MainLayout from "../../layouts/MainLayout";
import MainSection from "../../components/MainSection";
import DiscoverChart from "../../DiscoverChart";
import LazyLogo from "../../svgs/LazyLogo";
import SongSearchbar from "../../SongSearchbar";
import LikedChart from "../../LikedChart";
import RoutePath from "../../RoutePath";
import './HomePage.scss';

const discoverChartItemRender: Parameters<typeof DiscoverChart>[0]['renderer'] = (item) => {
    return <DiscoverChartItem {...item} />
}

const likedChartItemRender: Parameters<typeof LikedChart>[0]['renderer'] = (item) => {
    return <LikedChartItem {...item} />
}

const Table = (tableProps: React.DetailedHTMLProps<React.TableHTMLAttributes<HTMLTableElement>, HTMLTableElement>) => {
    return <table {...tableProps} />
};

function HomePage() {
    const { t } = useTranslation('Home');
    const { setQ } = useSearchContext();
    const navigate = useNavigate();

    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>(evt => {
        evt.preventDefault();
        var data = new FormData(evt.currentTarget).get('q');
        setQ(data as string);
        navigate(`${RoutePath['search']}?q=${data}&page=1`);
    }, [setQ, navigate]);

    return (
        <MainLayout className="aquamarine-theme">
            <MainSection className="homepage-region">
                <div className="main-logo-wrapper">
                    <LazyLogo className="main-logo" />
                </div>
                <SongSearchbar onSubmit={handleSubmit}/>
                <div className="chart-region">
                    <div className="main-chart-wrapper">
                        <div className="chart-title">{t("New Song Update")}</div>
                        <DiscoverChart className="discover-chart" maxResults={10}
                            Table={Table}
                            renderer={discoverChartItemRender}
                            thead={
                                <thead>
                                    <tr className="discover-chart-head">
                                        <th>{t("Title")}</th>
                                        <th>{t("Artist")}</th>
                                        <th>TJ</th>
                                        <th>KY</th>
                                    </tr>
                                </thead>
                            }
                        />
                        <Link className="fs-8" to={RoutePath['discoverChart']}>{t('More Chart')}</Link>
                    </div>
                    <div className="main-chart-wrapper">
                        <div className="chart-title">{t("Liked Song")}</div>
                        <DiscoverChart className="liked-chart" maxResults={10}
                            Table={Table}
                            renderer={likedChartItemRender}
                            thead={
                                <thead>
                                    <tr className="liked-chart-head">
                                        <th>{t("Rank")}</th>
                                        <th>{t("Title")}</th>
                                        <th>{t("Artist")}</th>
                                        <th>TJ</th>
                                        <th>KY</th>
                                    </tr>
                                </thead>
                            }
                        />
                        <Link className="fs-8" to={RoutePath['likedChart']}>{t("More Chart")}</Link>
                    </div>
                </div>
            </MainSection>
        </MainLayout>
    )
}

export default React.memo(HomePage);