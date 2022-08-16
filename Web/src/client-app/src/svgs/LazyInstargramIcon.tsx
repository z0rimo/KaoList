import React, { Suspense } from "react"

const Instargram = React.lazy(() => import('./InstargramIcon'));

function LazyInstargramIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="13"
            viewBox="0 0 13 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Instargram {...props} />
        </Suspense>
    )
}

export default React.memo(LazyInstargramIcon);