import React, { Suspense } from "react"

const MicIcon = React.lazy(() => import('./MicIcon'));

function LazyMicIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="10"
            height="14"
            viewBox="0 0 10 14"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <MicIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyMicIcon);