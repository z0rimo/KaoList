import React from "react";
import DiscoverChartItem from "./DiscoverChartItem";
import { IDiscoverChartItem } from "../DiscoverChart";

function DiscoverChartItemRenderer(item: IDiscoverChartItem) {
  return (
    <DiscoverChartItem {...item} />
  )
}

export default React.memo(DiscoverChartItemRenderer);