import React, { Suspense } from "react"

const LeftDoubleArrowIcon = React.lazy(() => import('./LeftDoubleArrowIcon'));

function LazyLeftDoubleArrowIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="9"
            height="11"
            viewBox="0 0 9 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <LeftDoubleArrowIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyLeftDoubleArrowIcon);