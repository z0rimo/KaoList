import React, { Suspense } from "react"

const VectorUpIcon = React.lazy(() => import('./VectorUpIcon'));

function LazyVectorUpIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="8"
            viewBox="0 0 12 8"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <VectorUpIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyVectorUpIcon);