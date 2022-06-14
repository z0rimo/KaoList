import React from "react";

function HomePage(props: React.HTMLAttributes<HTMLElement>) {
    return (
        <section id="main-section" {...props}>
        </section>
    )
}

export default React.memo(HomePage);