import React, { Suspense } from "react"

const CaretLeftSolidIcon = React.lazy(() => import('./CaretLeftSolidIcon'));

function LazyCaretLeftSolidIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="10"
            height="11"
            viewBox="0 0 10 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CaretLeftSolidIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCaretLeftSolidIcon);