import React, { Suspense } from "react"

const RecommendIcon = React.lazy(() => import('./RecommendIcon'));

function LazyRecommendIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="56"
            height="56"
            viewBox="0 0 56 56"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <RecommendIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyRecommendIcon);