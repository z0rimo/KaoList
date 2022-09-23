import React from "react";

function MainSection(props: React.HTMLAttributes<HTMLElement>) {
    return <section id='main-section' {...props} />
}

export default React.memo(MainSection);
