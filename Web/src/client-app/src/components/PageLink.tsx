import React from "react";
import { Link } from "react-router-dom";

interface IPageLinkProps {
    no: number;
    className?: string;
    base?: string;
    onClick?: (no: number, evt: React.MouseEvent<HTMLAnchorElement>) => void;
}

function PageLink(props: IPageLinkProps) {
    const handleClick = React.useCallback<React.MouseEventHandler<HTMLAnchorElement>>((evt) => {
        if (!props.onClick) {
            return;
        }
        evt.preventDefault();
        props.onClick(props.no, evt);
    }, [props]);

    return (
        <Link
            to={`${props.base}?page=${props.no}`}
            onClick={handleClick}
            className={props.className}
        >
            {props.no}
        </Link>
    );
}

export default React.memo(PageLink);