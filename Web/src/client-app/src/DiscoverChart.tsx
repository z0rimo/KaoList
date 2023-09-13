import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/kaolistApi';
import { useLocation } from 'react-router-dom';

export interface IDiscoverChartItem extends ISongSnippet {
    id: string;
}

type DiscoverChartProps = {
    date?: Date;
    Table: IRenderTableProps<IDiscoverChartItem>['Table'];
    thead: IRenderTableProps<IDiscoverChartItem>['thead'];
    renderer: IRenderTableProps<IDiscoverChartItem>['renderer'];
    maxResults?: number;
    setTotalResults?: React.Dispatch<React.SetStateAction<number>>;
} & React.TableHTMLAttributes<HTMLTableElement>;

function DiscoverChart(props: DiscoverChartProps) {
    const { date, Table, thead, renderer, maxResults, setTotalResults, ...rest } = props;
    const location = useLocation();

    const keySelector = React.useCallback((item: IDiscoverChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<IDiscoverChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const page = params.get('page') || '1';
            const response = await window.api.kaoList.charts.discoverChartList({
                date: date,
                page: parseInt(page, 10),
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.resources) {
                setItems(response.resources.map(item => ({
                    id: item.id!,
                    ...item.snippet!
                })));
            }

            if (response.pageInfo?.totalResults) {
                setTotalResults!(response.pageInfo.totalResults);
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

export default React.memo(DiscoverChart);