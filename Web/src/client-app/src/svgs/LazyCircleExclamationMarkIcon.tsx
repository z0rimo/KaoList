import React, { Suspense } from "react"

const CircleExclamationMarkIcon = React.lazy(() => import('./CircleExclamationMarkIcon'));

function LazyCircleExclamationMarkIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="7"
            height="7"
            viewBox="0 0 7 7"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CircleExclamationMarkIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCircleExclamationMarkIcon);