import React from "react";
import MainLayout from "../../layouts/MainLayout";
import MainSection from "../../components/MainSection";
import DiscoverChart, { IDiscoverChartItem } from "../../DiscoverChart";
import LazyLogo from "../../svgs/LazyLogo";
import './HomePage.scss';
import SongSearchbar from "../../SongSearchbar";
import LikedChart, { ILikedChartItem } from "../../LikedChart";
import RoutePath from "../../RoutePath";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

const DiscoverChartItem = React.memo((props: IDiscoverChartItem) => {
    return (
        <tr className="discover-chart-data">
            <td>{props.title}</td>
            <td>{props.singgers?.map(item => item.nickName).join(", ")}</td>
            <td>{(props?.karaoke && props?.karaoke['tj']?.no !== undefined) ? props?.karaoke['tj']?.no : "-"}</td>
            <td>{(props?.karaoke && props?.karaoke['kumyoung']?.no !== undefined) ? props?.karaoke['kumyoung']?.no : "-"}</td>
        </tr>
    )
});

const discoverChartItemRender: Parameters<typeof DiscoverChart>[0]['renderer'] = (item) => {
    return <DiscoverChartItem {...item} />
}

const LikedChartItem = React.memo((props: ILikedChartItem) => {
    return (
        <tr className="liked-chart-data">
            <td>{props.rank}</td>
            <td>{props.title}</td>
            <td>{props.singgers?.map(item => item.nickName).join(", ")}</td>
            <td>{(props?.karaoke && props?.karaoke['tj']?.no !== undefined) ? props?.karaoke['tj']?.no : "-"}</td>
            <td>{(props?.karaoke && props?.karaoke['kumyoung']?.no !== undefined) ? props?.karaoke['kumyoung']?.no : "-"}</td>
        </tr>
    )
});

const likedCharItemRender: Parameters<typeof LikedChart>[0]['renderer'] = (item, index) => {
    return <LikedChartItem {...item} rank={index + 1} />
}

const Table = (tableProps: React.DetailedHTMLProps<React.TableHTMLAttributes<HTMLTableElement>, HTMLTableElement>) => {
    return <table {...tableProps} />
};

function HomePage() {
    const { t } = useTranslation('Home');

    return (
        <MainLayout>
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
                        <LikedChart className="liked-chart" maxResults={10}
                            Table={Table}
                            renderer={likedCharItemRender}
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