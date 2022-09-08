import React, { Suspense } from "react"

const GoogleIcon = React.lazy(() => import('./GoogleIcon'));

function LazyGoogleIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="30"
            height="30"
            viewBox="0 0 30 30"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <GoogleIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyGoogleIcon);