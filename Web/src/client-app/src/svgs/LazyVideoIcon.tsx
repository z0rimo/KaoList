import React, { Suspense } from "react"

const VideoIcon = React.lazy(() => import('./VideoIcon'));

function LazyVideoIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="21"
            height="15"
            viewBox="0 0 21 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <VideoIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyVideoIcon);