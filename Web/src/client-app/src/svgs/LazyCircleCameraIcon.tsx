import React, { Suspense } from "react"

const CircleCameraIcon = React.lazy(() => import('./CircleCameraIcon'));

function LazyCircleCameraIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="30"
            height="30"
            viewBox="0 0 30 30"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CircleCameraIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCircleCameraIcon);