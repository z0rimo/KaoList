import React from "react";
import LazyTwitterIcon from "../svgs/LazyTwitterIcon";

function TwitterLinkIcon(props: React.AnchorHTMLAttributes<HTMLAnchorElement>) {
    return (
        <a className="link" {...props}>
            <LazyTwitterIcon />
        </a>
    )
}

export default React.memo(TwitterLinkIcon);