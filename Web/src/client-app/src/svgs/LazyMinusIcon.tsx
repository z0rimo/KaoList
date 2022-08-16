import React, { Suspense } from "react"

const Minus = React.lazy(() => import('./MinusIcon'));

function LazyMinusIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="2"
            viewBox="0 0 13 2"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Minus {...props} />
        </Suspense>
    )
}

export default React.memo(LazyMinusIcon);