import React, { Suspense } from "react"

const ChartIcon = React.lazy(() => import('./ChartIcon'));

function LazyChartIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="15"
            viewBox="0 0 17 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ChartIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyChartIcon);