import React from 'react';
import RenderTable, { IRenderTableProps } from './components/RenderTable';
import { ISongSnippet } from './api/models/ISongModels';

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

    const keySelector = React.useCallback((item: ILikedChartItem) => {
        return item.id;
    }, []);

    const [items, setItems] = React.useState<ILikedChartItem[]>([]);
    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.charts.likedChartList({
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

            if (response.pageInfo?.totalResults) {
                setTotalResults && setTotalResults(response.pageInfo.totalResults);
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