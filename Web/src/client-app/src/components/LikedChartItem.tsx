import React from "react";
import { ILikedChartItem } from "../LikedChart";

function LikedChartItem(props: ILikedChartItem) {
    let tjNo = "-";
    let kumyoungNo = "-";

    if (props.karaoke?.providerName === "tj") {
        tjNo = props.karaoke.no ?? "-";
    } else if (props.karaoke?.providerName === "kumyoung") {
        kumyoungNo = props.karaoke.no ?? "-";
    }

    return (
        <tr className="liked-chart-data">
            <td>{props.rank}</td>
            <td>{props.title}</td>
            <td>{props.songUsers?.map(item => item.nickname).join(", ")}</td>
            <td>{tjNo}</td>
            <td>{kumyoungNo}</td>
        </tr>
    )
};

export default React.memo(LikedChartItem);