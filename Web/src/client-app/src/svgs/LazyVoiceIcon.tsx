import React, { Suspense } from "react"

const VoiceIcon = React.lazy(() => import('./VoiceIcon'));

function LazyVoiceIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="15"
            viewBox="0 0 11 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <VoiceIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyVoiceIcon);