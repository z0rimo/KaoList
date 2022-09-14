import React, { Suspense } from "react"

const StarSolidIcon = React.lazy(() => import('./StarSolidIcon'));

function LazyStarSolidIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="23"
            height="23" viewBox="0 0 23 23"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <StarSolidIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyStarSolidIcon);