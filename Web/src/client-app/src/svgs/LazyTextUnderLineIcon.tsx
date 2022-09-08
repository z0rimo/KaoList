import React, { Suspense } from "react"

const TextUnderLineIcon = React.lazy(() => import('./TextUnderLineIcon'));

function LazyTextUnderLineIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="15"
            viewBox="0 0 13 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextUnderLineIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextUnderLineIcon);