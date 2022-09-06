import React, { Suspense } from "react"

const CaretDownIcon = React.lazy(() => import('./CaretDownIcon'));

function LazyCaretDownIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="10"
            height="6"
            viewBox="0 0 10 6"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CaretDownIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCaretDownIcon);