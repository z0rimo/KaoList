import React, { Suspense } from "react"

const CaretDownSolidIcon = React.lazy(() => import('./CaretDownSolidIcon'));

function LazyCaretDownSolidIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="10"
            height="6"
            viewBox="0 0 10 6"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CaretDownSolidIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCaretDownSolidIcon);