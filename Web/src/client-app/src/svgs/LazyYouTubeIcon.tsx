import React, { Suspense } from "react"

const YouTube = React.lazy(() => import('./YouTubeIcon'));

function LazyYouTubeIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="18"
            height="13"
            viewBox="0 0 18 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <YouTube {...props} />
        </Suspense>
    )
}

export default React.memo(LazyYouTubeIcon);