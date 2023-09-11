import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/kaolistApi';

export interface IDiscoverChartItem extends ISongSnippet {
    id: string;
}

type DiscoverChartProps = {
    date?: Date;
    Table: IRenderTableProps<IDiscoverChartItem>['Table'];
    thead: IRenderTableProps<IDiscoverChartItem>['thead'];
    renderer: IRenderTableProps<IDiscoverChartItem>['renderer'];
    maxResults?: number;
} & React.TableHTMLAttributes<HTMLTableElement>;

function DiscoverChart(props: DiscoverChartProps) {
    const { date, Table, thead, renderer, maxResults, ...rest } = props;

    const keySelector = React.useCallback((item: IDiscoverChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<IDiscoverChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.discoverChartList({
                date: date,
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