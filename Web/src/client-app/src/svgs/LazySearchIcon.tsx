import React, { Suspense } from "react"

const SearchIcon = React.lazy(() => import('./SearchIcon'));

function LazySearchIcon(props: React.SVGAttributes<SVGSVGElement>) {
    return (
        <Suspense fallback={(<svg width="33"
            height="33"
            viewBox="0 0 33 33"
            xmlns="http://www.w3.org/2000/svg"
            {...props}
        />)}>
            <SearchIcon {...props} />
        </Suspense>
    )
}

export default React.memo(LazySearchIcon);