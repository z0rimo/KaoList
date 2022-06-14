import React, { Suspense } from "react"

const HeaderLogo = React.lazy(() => import('./HeaderLogoIcon'));

function LazyHeaderLogoIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="52"
            height="30"
            viewBox="0 0 52 30"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <HeaderLogo {...props} />
        </Suspense>
    )
}

export default React.memo(LazyHeaderLogoIcon);