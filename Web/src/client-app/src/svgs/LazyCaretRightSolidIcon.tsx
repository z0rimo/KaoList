import React, { Suspense } from "react"

const CaretRightSolidIcon = React.lazy(() => import('./CaretRightSolidIcon'));

function LazyCaretRightSolidIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="10"
            height="11"
            viewBox="0 0 10 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CaretRightSolidIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCaretRightSolidIcon);