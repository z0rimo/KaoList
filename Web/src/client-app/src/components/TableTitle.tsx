import React from "react";

type TableTitleProps = {
    title?: string;
    date?: string;
    option?: React.ReactNode;
}

function TableTitle(props: TableTitleProps) {
    return (
        <div className="table-title-wrapper">
            <div>
                <p className="table-title">
                    {props.title}
                </p>
                {props.date && (
                    <p className="table-date">
                        {props.date}
                    </p>
                )}
            </div>
            {props.option}
        </div>
    )
}

export default React.memo(TableTitle);