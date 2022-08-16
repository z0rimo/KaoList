import React, { Suspense } from "react"

const TableIcon = React.lazy(() => import('./TableIcon'));

function LazyTableIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="15"
            viewBox="0 0 17 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TableIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTableIcon);