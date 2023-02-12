import React, { Suspense } from "react"

const ChartColumnIcon = React.lazy(() => import('./ChartColumnIcon'));

function LazyChartColumnIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="15"
            viewBox="0 0 17 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ChartColumnIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyChartColumnIcon);