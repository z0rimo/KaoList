import React, { Suspense } from "react"

const CaretDoubleRightIcon = React.lazy(() => import('./CaretDoubleRightIcon'));

function LazyCaretDoubleRightIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="11"
            viewBox="0 0 11 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CaretDoubleRightIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCaretDoubleRightIcon);