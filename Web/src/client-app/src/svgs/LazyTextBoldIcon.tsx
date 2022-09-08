import React, { Suspense } from "react"

const TextBoldIcon = React.lazy(() => import('./TextBoldIcon'));

function LazyTextBoldIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="14"
            viewBox="0 0 12 14"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextBoldIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextBoldIcon);