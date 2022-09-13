import React, { Suspense } from "react"

const PlaylistTitleIcon = React.lazy(() => import('./PlaylistTitleIcon'));

function LazyPlaylistTitleIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="20"
            height="21"
            viewBox="0 0 20 21"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <PlaylistTitleIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyPlaylistTitleIcon);