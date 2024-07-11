import React from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import DiscoverChart from "../../DiscoverChart";
import LikedChart from "../../LikedChart";
import RoutePath from "../../RoutePath";
import Table from "../../components/Table";
import DiscoverChartItemRenderer from "../../components/DiscoverChartItemRenderer";
import LikedChartItemRenderer from "../../components/LikedChartItemRenderer";

interface IChartSection extends Omit<React.HTMLAttributes<HTMLDivElement>, 'children'> {

}

function ChartSection({
  className,
  ...rest
}: IChartSection) {
  const { t } = useTranslation('Home');

  return (
    <div className="chart-region" {...rest}>
      <div className="main-chart-wrapper">
        <div className="chart-title">{t("New Song Update")}</div>
        {<DiscoverChart
          className="discover-chart"
          maxResults={10}
          Table={Table}
          renderer={DiscoverChartItemRenderer}
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
        />}
        <Link className="fs-8" to={RoutePath['discoverChart']}>{t('More Chart')}</Link>
      </div>
      <div className="main-chart-wrapper">
        <div className="chart-title">{t("Liked Song")}</div>
        {<LikedChart
          className="liked-chart"
          maxResults={10}
          Table={Table}
          renderer={LikedChartItemRenderer}
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
        />}
        <Link className="fs-8" to={RoutePath['likedChart']}>{t("More Chart")}</Link>
      </div>
    </div>
  );
};

export default React.memo(ChartSection);