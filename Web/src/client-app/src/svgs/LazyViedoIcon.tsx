import React, { Suspense } from "react"

const ViedoIcon = React.lazy(() => import('./ViedoIcon'));

function LazyViedoIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="21"
            height="15"
            viewBox="0 0 21 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ViedoIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyViedoIcon);