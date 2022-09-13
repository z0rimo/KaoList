import React, { Suspense } from "react"

const PlusIcon = React.lazy(() => import('./PlusIcon'));

function LazyPlusIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="11"
            viewBox="0 0 11 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <PlusIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyPlusIcon);