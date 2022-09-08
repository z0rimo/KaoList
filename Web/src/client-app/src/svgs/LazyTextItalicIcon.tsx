import React, { Suspense } from "react"

const TextItalicIcon = React.lazy(() => import('./TextItalicIcon'));

function LazyTextItalicIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="15"
            viewBox="0 0 11 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextItalicIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextItalicIcon);