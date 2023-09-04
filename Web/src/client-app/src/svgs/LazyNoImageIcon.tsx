import React, { Suspense } from "react"

const NoImageIcon = React.lazy(() => import('./NoImageIcon'));

function LazyNoImageIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="100"
            height="100"
            viewBox="0 0 100 100"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <NoImageIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyNoImageIcon);