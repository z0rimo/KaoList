import React, { Suspense } from "react"

const TextSizeIcon = React.lazy(() => import('./TextSizeIcon'));

function LazyTextSizeIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="15"
            viewBox="0 0 13 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextSizeIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextSizeIcon);