import React, { Suspense } from "react"

const Logo = React.lazy(() => import('./Logo'));

function LazyLogo(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="300"
            height="60"
            viewBox="0 0 300 60"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <Logo {...props} />
        </Suspense>
    )
}

export default React.memo(LazyLogo);