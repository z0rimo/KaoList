import React, { Suspense } from "react"

const KakaoIcon = React.lazy(() => import('./KakaoIcon'));

function LazyKakaoIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="30"
            height="30"
            viewBox="0 0 30 30"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <KakaoIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazyKakaoIcon);