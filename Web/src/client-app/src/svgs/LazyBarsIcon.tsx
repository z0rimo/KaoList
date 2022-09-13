import React, { Suspense } from "react"

const BarsIcon = React.lazy(() => import('./BarsIcon'));

function LazyBarsIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="9"
            height="9"
            viewBox="0 0 9 9"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <BarsIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyBarsIcon);