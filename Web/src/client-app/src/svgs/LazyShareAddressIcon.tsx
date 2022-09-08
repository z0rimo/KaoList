import React, { Suspense } from "react"

const ShareAddressIcon = React.lazy(() => import('./ShareAddressIcon'));

function LazyShareAddressIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="9"
            height="11"
            viewBox="0 0 9 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ShareAddressIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyShareAddressIcon);