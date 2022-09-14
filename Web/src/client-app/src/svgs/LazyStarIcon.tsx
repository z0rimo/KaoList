import React, { Suspense } from "react"

const StarIcon = React.lazy(() => import('./StarIcon'));

function LazyStarIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="20"
            height="20"
            viewBox="0 0 20 20"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <StarIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyStarIcon);