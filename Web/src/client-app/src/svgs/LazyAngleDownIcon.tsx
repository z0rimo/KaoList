import React, { Suspense } from "react"

const AngleDownIcon = React.lazy(() => import('./AngleDownIcon'));

function LazyAngleDownIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="8"
            viewBox="0 0 12 8"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <AngleDownIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyAngleDownIcon);