import React, { Suspense } from "react"

const Twitter = React.lazy(() => import('./TwitterIcon'));

function LazyTwitterIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="13"
            viewBox="0 0 17 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Twitter {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTwitterIcon);