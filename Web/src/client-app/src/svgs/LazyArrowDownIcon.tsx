import React, { Suspense } from "react"

const ArrowDownIcon = React.lazy(() => import('./ArrowDownIcon'));

function LazyArrowDownIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="7"
            height="10"
            viewBox="0 0 7 10"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ArrowDownIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyArrowDownIcon);