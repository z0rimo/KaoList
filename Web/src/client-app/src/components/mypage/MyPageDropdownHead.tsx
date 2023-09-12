import React from "react";

interface IMyPageItemDropdownHeadProps {
    title?: string;
    caseName?: string;
    count?: number;
    button?: React.ReactNode;
}

function MyPageItemDropdownHead(props: IMyPageItemDropdownHeadProps) {
    const { title, caseName, count, button } = props;

    return (
        <div className="mypage-item dropdown-head">
            <p className="item-title">{title}</p>
            <div className="button-wrapper">
                <div className="case-wrapper">
                    <p className="case-name">{caseName}:</p>
                    <p className="count">{count?.toLocaleString()}</p>
                </div>
                {button}
            </div>
        </div>
    )
}

export default React.memo(MyPageItemDropdownHead);