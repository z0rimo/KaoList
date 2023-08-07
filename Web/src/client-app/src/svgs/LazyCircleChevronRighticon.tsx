import React, { Suspense } from "react"

const CircleChevronRightIcon = React.lazy(() => import('./CircleChevronRightIcon'));

function LazyCircleChevronRightIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="20"
            height="20"
            viewBox="0 0 20 20"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CircleChevronRightIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCircleChevronRightIcon);