import React from "react";
import RenderTable, { IRenderTableProps } from "./RenderTable";
import { useLocation } from "react-router-dom";
import { ISongSnippet } from "../api/models/ISongModels";

export interface ISongDetailOtherMySongItem extends ISongSnippet {
    id: string;
}

type SongDetailOtherMySongProps = {
    Table: IRenderTableProps<ISongDetailOtherMySongItem>['Table'];
    thead: IRenderTableProps<ISongDetailOtherMySongItem>['thead'];
    renderer: IRenderTableProps<ISongDetailOtherMySongItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function SongDetailOtherMySong(props: SongDetailOtherMySongProps) {
    const { Table, thead, renderer, maxResults, ...rest } = props;
    const location = useLocation();

    const keySelector = React.useCallback((item: ISongDetailOtherMySongItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ISongDetailOtherMySongItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const id = params.get('id');
            const response = await window.api.kaoList.songs.songDetail({
                id: typeof id === 'string' ? id : undefined,
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.otherMySongs) {
                setItems(response.otherMySongs.map(item => ({
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

export default React.memo(SongDetailOtherMySong);