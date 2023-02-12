import React, { Suspense } from "react"

const ImageIcon = React.lazy(() => import('./ImageIcon'));

function LazyImageIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="17"
            height="15"
            viewBox="0 0 17 15"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <ImageIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyImageIcon);