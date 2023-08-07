import React, { Suspense } from "react"

const GearIcon = React.lazy(() => import('./GearIcon'));

function LazyGearIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="12"
            height="13"
            viewBox="0 0 12 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <GearIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyGearIcon);