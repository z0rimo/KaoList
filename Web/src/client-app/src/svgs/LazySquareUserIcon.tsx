import React, { Suspense } from "react"

const SquareUserIcon = React.lazy(() => import('./SquareUserIcon'));

function LazySquareUserIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="230"
            height="230"
            viewBox="0 0 230 230"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <SquareUserIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazySquareUserIcon);