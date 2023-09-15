import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/kaolistApi';
import { useLocation } from 'react-router-dom';

export interface ILikedChartItem extends ISongSnippet {
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
    setTotalResults?: React.Dispatch<React.SetStateAction<number>>;
} & React.TableHTMLAttributes<HTMLTableElement>;

function LikedChart(props: LikedChartProps) {
    const { startDate, endDate, Table, thead, renderer, maxResults, setTotalResults, ...rest } = props;
    const location = useLocation();

    const keySelector = React.useCallback((item: ILikedChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ILikedChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const params = new URLSearchParams(location.search);
            const page = params.get('page') || '1';
            const response = await window.api.kaoList.charts.likedChartList({
                startDate: startDate,
                endDate: endDate,
                page: parseInt(page, 10),
                part: ['snippet'],
                maxResults: maxResults ?? 10
            });

            if (response.resources) {
                const currentPage = parseInt(page, 10);
                const rankedItems = response.resources.map((item, index) => ({
                    id: item.id!,
                    rank: (currentPage - 1) * (maxResults ?? 10) + index + 1,
                    ...item.snippet!
                }));

                setItems(rankedItems);
            }

            if (response.pageInfo?.totalResults && setTotalResults) {
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

export default React.memo(LikedChart);