import React from "react";
import TwitterLinkIcon from "../TwitterLinkIcon";

function FooterInfo() {
    return (
        <div className="info">
            <p>ⓒ 2022 KaoList</p>
            <div className="sns-item">
                <TwitterLinkIcon href={process.env.REACT_APP_KAOLIST_TWITTER_LINK} target="_blank" rel="noreferrer" />
            </div>
        </div>
    )
}

export default React.memo(FooterInfo);