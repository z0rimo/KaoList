import React, { Suspense } from "react"

const ReportIcon = React.lazy(() => import('./ReportIcon'));

function LazyReportIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="19"
            height="14"
            viewBox="0 0 19 14"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ReportIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyReportIcon);