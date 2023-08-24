import React from "react";
import MainLayout from "../../layouts/MainLayout";
import MainSection from "../../components/MainSection";
import DiscoverChart from "../../DiscoverChart";
import LazyLogo from "../../svgs/LazyLogo";
import './HomePage.scss';
import SongSearchbar from "../../SongSearchbar";
import LikedChart from "../../LikedChart";
import RoutePath from "../../RoutePath";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import DiscoverChartItem from "../../components/DiscoverChartItem";
import LikedChartItem from "../../components/LikedChartItem";

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

    return (
        <MainLayout className="aquamarine-theme">
            <MainSection className="homepage-region">
                <div className="main-logo-wrapper">
                    <LazyLogo className="main-logo" />
                </div>
                <SongSearchbar />
                <div className="chart-region">
                    <div className="chart-wrapper">
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
                    <div className="chart-wrapper">
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