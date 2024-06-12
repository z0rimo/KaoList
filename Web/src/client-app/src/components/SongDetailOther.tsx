import React from 'react';
import RenderTable, { IRenderTableProps } from './RenderTable';
import { useLocation } from 'react-router-dom';
import { ISongSnippet } from '../api/models/ISongModels';

export interface ISongDetailOtherItem extends ISongSnippet {
    id: string;
}

type SongDetailOtherProps = {
    Table: IRenderTableProps<ISongDetailOtherItem>['Table'];
    thead: IRenderTableProps<ISongDetailOtherItem>['thead'];
    renderer: IRenderTableProps<ISongDetailOtherItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function SongDetailOther(props: SongDetailOtherProps) {
    const { Table, thead, renderer, maxResults, ...rest } = props;
    const location = useLocation();
    
    const keySelector = React.useCallback((item: ISongDetailOtherItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ISongDetailOtherItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const id = params.get('id');
            const response = await window.api.kaoList.songs.songDetail({
                id: typeof id === 'string' ? id : undefined,
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.otherSongs) {
                setItems(response.otherSongs.map(item => ({
                    id: item.id!,
                    ...item.snippet!
                })));
            }
        })();
        // eslint-disable-next-line
    }, []);

    if (items.length === 0) {
        return null;
    }

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

export default React.memo(SongDetailOther);