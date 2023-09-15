import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/kaolistApi';

export interface ITotalLikedChartItem extends ISongSnippet {
    id: string;
    rank?: number;
}

type TotalLikedChartProps = {
    startDate?: Date;
    endDate?: Date;
    Table: IRenderTableProps<ITotalLikedChartItem>['Table'];
    thead: IRenderTableProps<ITotalLikedChartItem>['thead'];
    renderer: IRenderTableProps<ITotalLikedChartItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function TotalLikedChart(props: TotalLikedChartProps) {
    const { startDate, endDate, Table, thead, renderer, maxResults, ...rest } = props;

    const keySelector = React.useCallback((item: ITotalLikedChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ITotalLikedChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.likedTotalChartList({
                startDate: startDate,
                endDate: endDate,
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.resources) {
                const rankedItems = response.resources.map((item, index) => ({
                    id: item.id!,
                    rank: index + 1,
                    ...item.snippet!
                }));

                setItems(rankedItems);
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

export default React.memo(TotalLikedChart);