
import React from "react";

export interface IRenderTableProps<T> extends Omit<
    React.DetailedHTMLProps<React.TableHTMLAttributes<HTMLTableElement>, HTMLTableElement>,
    'children'
> {
    thead: React.ReactNode;
    items: Array<T>;
    Table: React.ComponentType<
        React.DetailedHTMLProps<React.TableHTMLAttributes<HTMLTableElement>, HTMLTableElement>
    >;
    renderer: (item: T, index: number, array: T[]) => JSX.Element | null;
    keySelector: (item: T) => React.Key;
};

function RenderTable<T>(props: IRenderTableProps<T>) {
    const { className, thead, items, renderer, keySelector, Table, ...rest } = props;
    return (
        <Table {...rest}>
            {thead}
            <tbody>
                {items.map((item, index, array) => {
                    const el = renderer(item, index, array);
                    if (el === null) {
                        return null;
                    }

                    return React.cloneElement(
                        el, { key: keySelector(item) }
                    );
                })}
            </tbody>
        </Table>
    )
}

// TODO
// While going through React.memo, there is a problem that T cannot be inferred normally and is displayed as unknown,
// so it is forced to cast to typeof RenderTable.
//
// If that problem is fixed or can be solved in some other way, it should be fixed.
export default React.memo(RenderTable) as typeof RenderTable;
