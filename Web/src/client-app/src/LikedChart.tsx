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
            const response = await window.api.kaoList.charts.list({
                startDate: startDate,
                endDate: endDate,
                part: ['snippet'],
                type: 'liked',
                maxResults: maxResults ?? 10
            });

            setItems(response.items.map(item => {
                return ({
                    id: item.id,
                    ...item.snippet!
                })
            }))
        })();
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