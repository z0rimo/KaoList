import React, { Suspense } from "react"

const LinkSimpleIcon = React.lazy(() => import('./LinkSimpleIcon'));

function LazyLinkSimpleIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="11"
            height="7"
            viewBox="0 0 11 7"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <LinkSimpleIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyLinkSimpleIcon);