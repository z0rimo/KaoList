import React, { Suspense } from "react"

const Spotify = React.lazy(() => import('./SpotifyIcon'));

function LazySpotifyIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="13"
            viewBox="0 0 13 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Spotify {...props} />
        </Suspense>
    )
}

export default React.memo(LazySpotifyIcon);