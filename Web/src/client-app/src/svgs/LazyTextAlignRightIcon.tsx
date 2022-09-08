import React, { Suspense } from "react"

const TextAlignRightIcon = React.lazy(() => import('./TextAlignRightIcon'));

function LazyTextAlignRightIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="15"
            viewBox="0 0 15 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextAlignRightIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextAlignRightIcon);