import React, { Suspense } from "react"

const AngleUpIcon = React.lazy(() => import('./AngleUpIcon'));

function LazyAngleUpIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="8"
            viewBox="0 0 12 8"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <AngleUpIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyAngleUpIcon);