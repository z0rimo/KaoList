import React from "react";

interface IPageLinkProps {
    no: number;
    className?: string;
    base: string;
    onClick?: (no: number, evt: React.MouseEvent<HTMLAnchorElement>) => void;
}

function PageLink({ no, className, base, onClick }: IPageLinkProps) {
    const handleClick = (evt: React.MouseEvent<HTMLAnchorElement>) => {
        evt.preventDefault(); 
        if (onClick) {
            onClick(no, evt);
        }
    };

    return (
        <a href={base}
            className={className}
            onClick={handleClick}
            style={{ textDecoration: "none" }}>
            {no}
        </a>
    );
}


export default React.memo(PageLink);