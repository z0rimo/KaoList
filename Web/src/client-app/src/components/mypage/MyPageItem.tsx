import React from "react";
import ClassNameHelper from "../../ClassNameHelper";

export interface IMyPageItemProps {
    className?: string;
    title?: string;
    information?: React.ReactNode;
    date?: Date;
    options?: React.ReactNode;
}

function MyPageItem(props: IMyPageItemProps) {
    const { title, information, options, className } = props;

    return (
        <div className={ClassNameHelper.concat("mypage-item", className)}>
            <div className="left-item">
                {title &&
                    <p className="item-title">
                        {title}
                    </p>
                }
                {information}
            </div>
            <div className="right-item">
                {options}
            </div>
        </div>
    )
}

export default React.memo(MyPageItem);