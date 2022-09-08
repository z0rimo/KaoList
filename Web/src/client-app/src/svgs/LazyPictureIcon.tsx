import React, { Suspense } from "react"

const PictureIcon = React.lazy(() => import('./PictureIcon'));

function LazyPictureIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="15"
            viewBox="0 0 17 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <PictureIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyPictureIcon);