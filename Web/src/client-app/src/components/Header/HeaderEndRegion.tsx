import React, { HtmlHTMLAttributes } from "react";

function HeaderEndRegion(props: HtmlHTMLAttributes<HTMLDivElement>) {
    return (
        <div className="header-end-region" {...props}/>
    )
}

export default React.memo(HeaderEndRegion);