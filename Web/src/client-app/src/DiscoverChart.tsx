import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { IChartSnippet } from './api/kaolistApi';

export interface IDiscoverChartItem extends IChartSnippet {
    id: string;
}

type DiscoverChartProps = {
    startDate?: Date;
    endDate?: Date;
    Table: IRenderTableProps<IDiscoverChartItem>['Table'];
    thead: IRenderTableProps<IDiscoverChartItem>['thead'];
    renderer: IRenderTableProps<IDiscoverChartItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function DiscoverChart(props: DiscoverChartProps) {
    const { startDate, endDate, Table, thead, renderer, maxResults, ...rest } = props;

    const keySelector = React.useCallback((item: IDiscoverChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<IDiscoverChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.discoverChartList({
                date: new Date().toISOString().split('T')[0],
                part: ['snippet'],
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

export default React.memo(DiscoverChart);