import React, { Suspense } from "react"

const CircleProfile = React.lazy(() => import('./CircleProfileIcon'));

function LazyCircleProfileIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="120"
            height="120"
            viewBox="0 0 120 120"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CircleProfile {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCircleProfileIcon);