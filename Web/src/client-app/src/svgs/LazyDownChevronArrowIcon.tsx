import React, { Suspense } from "react"

const DownChevronArrow = React.lazy(() => import('./DownChevronArrowIcon'));

function LazyDownChevronArrowIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="9"
            viewBox="0 0 15 9"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <DownChevronArrow {...props} />
        </Suspense>
    )
}

export default React.memo(LazyDownChevronArrowIcon);