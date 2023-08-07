import React, { HtmlHTMLAttributes } from "react";

function HeaderStartRegion(props: HtmlHTMLAttributes<HTMLDivElement>) {
    return (
        <div className="header-start-region" {...props}/>
    )
}

export default React.memo(HeaderStartRegion);