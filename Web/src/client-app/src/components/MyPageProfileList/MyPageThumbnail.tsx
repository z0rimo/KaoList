import React from "react";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import LazyCircleCameraIcon from "../../svgs/LazyCircleCameraIcon";

function MyPageThumbnail(props: React.HTMLAttributes<HTMLDivElement>) {
    return (
        <div className="thumbnail-wrapper" {...props}>
            <LazyCircleProfileIcon className="thumbnail" />
            <button className="thumbnail-change">
                <LazyCircleCameraIcon className="rounded-circle circle-camera" />
            </button>
        </div>
    )
}

export default React.memo(MyPageThumbnail);