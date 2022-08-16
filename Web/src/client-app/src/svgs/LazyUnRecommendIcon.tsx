import React, { Suspense } from "react"

const UnRecommendIcon = React.lazy(() => import('./UnRecommendIcon'));

function LazyUnRecommendIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="56"
            height="56"
            viewBox="0 0 56 56"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <UnRecommendIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyUnRecommendIcon);