import React, { Suspense } from "react"

const SpeechBubbleIcon = React.lazy(() => import('./SpeechBubbleIcon'));

function LazySpeechBubbleIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="11"
            viewBox="0 0 11 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <SpeechBubbleIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazySpeechBubbleIcon);