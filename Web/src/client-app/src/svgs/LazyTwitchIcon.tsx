import React, { Suspense } from "react"

const Twitch = React.lazy(() => import('./TwitchIcon'));

function LazyTwitchIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="13"
            viewBox="0 0 13 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Twitch {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTwitchIcon);