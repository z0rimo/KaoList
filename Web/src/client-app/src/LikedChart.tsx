import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { IChartSnippet } from './api/kaolistApi';

export interface ILikedChartItem extends IChartSnippet {
    id: string;
    rank?: number;
}

type LikedChartProps = {
    startDate?: Date;
    endDate?: Date;
    Table: IRenderTableProps<ILikedChartItem>['Table'];
    thead: IRenderTableProps<ILikedChartItem>['thead'];
    renderer: IRenderTableProps<ILikedChartItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function LikedChart(props: LikedChartProps) {
    const { startDate, endDate, Table, thead, renderer, maxResults, ...rest } = props;

    const keySelector = React.useCallback((item: ILikedChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ILikedChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.discoverChartList({
                date: new Date().toISOString().split('T')[0],
                part: ['snippet'],
                //type: 'discovered',
                maxResults: maxResults ?? 10
            });

            if (response.resources) {
                setItems(response.resources.map(item => ({
                    id: item.id!,
                    ...item.snippet!
                })));
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

export default React.memo(LikedChart);