import React, { Suspense } from "react"

const X = React.lazy(() => import('./XIcon'));

function LazyXIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="15"
            viewBox="0 0 15 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <X {...props} />
        </Suspense>
    )
}

export default React.memo(LazyXIcon);