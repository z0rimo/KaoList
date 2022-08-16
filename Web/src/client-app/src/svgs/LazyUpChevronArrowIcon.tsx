import React, { Suspense } from "react"

const UpChevronArrow = React.lazy(() => import('./UpChevronArrowIcon'));

function LazyUpChevronArrowIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="9"
            viewBox="0 0 15 9"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <UpChevronArrow {...props} />
        </Suspense>
    )
}

export default React.memo(LazyUpChevronArrowIcon);