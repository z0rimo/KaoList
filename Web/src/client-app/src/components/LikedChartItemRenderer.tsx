import React from "react";
import DiscoverChartItem from "./DiscoverChartItem";
import { ILikedChartItem } from "../LikedChart";

function LikedChartItemRenderer(item: ILikedChartItem) {
  return (
    <DiscoverChartItem {...item} />
  )
}

export default React.memo(LikedChartItemRenderer);