import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/kaolistApi';
import { useSearchContext } from './contexts/SearchContext';
import { useLocation } from 'react-router-dom';

export interface ISongSearchListItem extends ISongSnippet {
    id: string;
}

type SongSearchListProps = {
    Table: IRenderTableProps<ISongSearchListItem>['Table'];
    thead: IRenderTableProps<ISongSearchListItem>['thead'];
    renderer: IRenderTableProps<ISongSearchListItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function SongSearchList(props: SongSearchListProps) {
    const location = useLocation();
    const { Table, thead, renderer, maxResults, ...rest } = props;

    const keySelector = React.useCallback((item: ISongSearchListItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ISongSearchListItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const q = params.get('q');
            const response = await window.api.kaoList.searchs.songSearchList({
                q: typeof q === 'string' ? [q] : undefined,
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.items) {
                setItems(response.items.map(item => {
                    const id = item.id?.id ?? '';  
                    return {
                        id,
                        ...item.snippet!
                    };
                }));
            }
        })();
        // eslint-disable-next-line
    }, []);

    return (
        <RenderTable
            Table={Table}
            items={items}
            keySelector={keySelector}
            renderer={renderer}
            thead={thead}
            {...rest}
        />
    )
}

export default React.memo(SongSearchList);