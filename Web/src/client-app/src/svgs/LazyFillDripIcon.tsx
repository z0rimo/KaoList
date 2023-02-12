import React, { Suspense } from "react"

const FillDripIcon = React.lazy(() => import('./FillDripIcon'));

function LazyFillDripIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="15"
            viewBox="0 0 15 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <FillDripIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyFillDripIcon);