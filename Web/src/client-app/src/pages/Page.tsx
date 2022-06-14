import React from "react";

function Page(props: React.HTMLAttributes<HTMLElement>) {
    return <section id='main-section' {...props} />
}

export default React.memo(Page);