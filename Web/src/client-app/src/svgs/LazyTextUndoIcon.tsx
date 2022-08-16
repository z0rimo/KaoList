import React, { Suspense } from "react"

const TextUndoIcon = React.lazy(() => import('./TextUndoIcon'));

function LazyTextUndoIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="15"
            viewBox="0 0 15 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextUndoIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextUndoIcon);