import React from "react";
import DateOptionFormatter from "../DateOptionFormatter";

function MyPageDate(props: { children?: Date }) {
    if (props.children === undefined) {
        return null;
    }

    return (
        <p>{props.children?.toLocaleDateString(navigator.language, DateOptionFormatter.short)}</p>
    )
}

export default React.memo(MyPageDate);