import React from "react";
import MyPageDate from "./MyPageDate";
import MyPageItem, { IMyPageItemProps } from "./MyPageItem";

function MyPageItemWithDate(props: IMyPageItemProps) {
    const { date, options, ...rest } = props;

    return (
        <MyPageItem {...rest}
            options={
                <>
                    <MyPageDate>{date}</MyPageDate>
                    {options}
                </>
            }
        />
    )
}

export default React.memo(MyPageItemWithDate);