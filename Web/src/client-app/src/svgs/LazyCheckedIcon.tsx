import React, { Suspense } from "react"

const CheckedIcon = React.lazy(() => import('./CheckedIcon'));

function LazyCheckedIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="18"
            height="18"
            viewBox="0 0 18 18"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CheckedIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCheckedIcon);