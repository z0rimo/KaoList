import React, { Suspense } from "react"

const Pencil = React.lazy(() => import('./PencilIcon'));

function LazyPencilIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="13"
            height="13"
            viewBox="0 0 13 13"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Pencil {...props} />
        </Suspense>
    )
}

export default React.memo(LazyPencilIcon);