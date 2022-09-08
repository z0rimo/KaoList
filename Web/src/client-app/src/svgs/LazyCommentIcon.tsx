import React, { Suspense } from "react"

const CommentIcon = React.lazy(() => import('./CommentIcon'));

function LazyCommentIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="14"
            height="11"
            viewBox="0 0 14 11"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <CommentIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyCommentIcon);