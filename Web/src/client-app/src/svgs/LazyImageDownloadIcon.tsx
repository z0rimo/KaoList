import React, { Suspense } from "react"

const ImageDownloadIcon = React.lazy(() => import('./ImageDownloadIcon'));

function LazyImageDownloadIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="9"
            viewBox="0 0 13 9"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ImageDownloadIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyImageDownloadIcon);