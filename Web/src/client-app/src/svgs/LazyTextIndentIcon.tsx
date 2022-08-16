import React, { Suspense } from "react"

const TextIndentIcon = React.lazy(() => import('./TextIndentIcon'));

function LazyTextIndentIcon(props: React.HTMLAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="15"
            height="15"
            viewBox="0 0 15 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <TextIndentIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyTextIndentIcon);