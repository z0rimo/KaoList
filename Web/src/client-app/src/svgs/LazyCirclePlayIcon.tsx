import React, { Suspense } from "react"

const CirclePlayIcon = React.lazy(() => import('./CirclePlayIcon'));

function LazyCirclePlayIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="17"
            viewBox="0 0 17 17"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CirclePlayIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCirclePlayIcon);