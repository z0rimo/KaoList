import React, { Suspense } from "react"

const BoldIcon = React.lazy(() => import('./BoldIcon'));

function LazyBoldIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="14"
            viewBox="0 0 12 14"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <BoldIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyBoldIcon);