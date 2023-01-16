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

    const [items, setItems] = React.useState<IDiscoverChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.list({
                startDate: startDate,
                endDate: endDate,
                part: ['snippet'],
                type: 'discovered',
                maxResults: maxResults ?? 10
            });

            setItems(response.items.map(item => {
                return ({
                    id: item.id,
                    ...item.snippet!
                })
            }))
        })();
        // eslint-disable-next-line
    }, []);

    return (
        <RenderTable
            Table={Table}
            items={items}
            keySelector={item => item.id}
            renderer={renderer}
            thead={thead}
            {...rest}
        />
    )
}

export default React.memo(DiscoverChart);

