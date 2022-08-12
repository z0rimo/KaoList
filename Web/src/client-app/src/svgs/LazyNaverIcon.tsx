import React, { Suspense } from "react"

const NaverIcon = React.lazy(() => import('./NaverIcon'));

function LazyNaverIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="30"
            height="30"
            viewBox="0 0 30 30"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <NaverIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyNaverIcon);