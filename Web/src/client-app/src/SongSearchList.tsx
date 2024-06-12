import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { useLocation } from 'react-router-dom';
import { ISongSnippet } from './api/models/ISongModels';

export interface ISongSearchListItem extends ISongSnippet {
    id: string;
}

type SongSearchListProps = {
    Table: IRenderTableProps<ISongSearchListItem>['Table'];
    thead: IRenderTableProps<ISongSearchListItem>['thead'];
    renderer: IRenderTableProps<ISongSearchListItem>['renderer'];
    maxResults?: number;
    setTotalResults: React.Dispatch<React.SetStateAction<number>>;
} & React.TableHTMLAttributes<HTMLTableElement>;

function SongSearchList(props: SongSearchListProps) {
    const location = useLocation();
    const { Table, thead, renderer, maxResults, setTotalResults, ...rest } = props;
    const [items, setItems] = React.useState<ISongSearchListItem[]>([]);

    const keySelector = React.useCallback((item: ISongSearchListItem) => {
        return item.id;
    }, []);

    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const q = params.get('q');
            const page = params.get('page') || '1';
            const response = await window.api.kaoList.searchs.songSearchList({
                q: typeof q === 'string' ? [q] : undefined,
                page: parseInt(page, 10),
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

            if (response.pageInfo?.totalResults) {
                setTotalResults(response.pageInfo.totalResults);
            }
        })();

        window.scrollTo(0, 0);
        // eslint-disable-next-line
    }, [location.search]);
    
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