import React, { Suspense } from "react"

const AnswerCommentIcon = React.lazy(() => import('./AnswerCommentIcon'));

function LazyAnswerCommentIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="7"
            height="5"
            viewBox="0 0 7 5"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <AnswerCommentIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyAnswerCommentIcon);