import React from "react";
import { ISongSnippet } from "./api/kaolistApi";

export interface ISongDetailItem extends ISongSnippet {
    id: string;
}

function SongDetail() {
    return (
        <div></div>
    )
}

export default React.memo(SongDetail);