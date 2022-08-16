import React, { Suspense } from "react"

const OverlappingRectIcon = React.lazy(() => import('./OverlappingRectIcon'));

function LazyOverlappingRectIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="11"
            viewBox="0 0 11 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <OverlappingRectIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyOverlappingRectIcon);