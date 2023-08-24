import React from "react";

export interface ISearchContext {
    q?: string | string[];
    setQ: (q: string | string[]) => void;
}

export function useSearchContext() {
    const [q, setQ] = React.useState<string | string[]>('');
    
    return React.useMemo<ISearchContext>(() => {
        return {
            q: q,
            setQ: setQ
        } as ISearchContext;
    }, [q]);
}

const SearchContext = React.createContext<ISearchContext>({
    setQ: () => { }
});

export default SearchContext;