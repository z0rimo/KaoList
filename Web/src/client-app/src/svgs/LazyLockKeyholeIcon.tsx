import React, { Suspense } from "react"

const LockKeyholeIcon = React.lazy(() => import('./LockKeyholeIcon'));

function LazyLockKeyholeIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="13"
            viewBox="0 0 11 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <LockKeyholeIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyLockKeyholeIcon);