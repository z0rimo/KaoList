import React from "react";

interface IPageLinkProps {
    no: number;
    className?: string;
    base: string;
    onClick?: (no: number, evt: React.MouseEvent<HTMLAnchorElement>) => void;
}

function PageLink({ no, className, base, onClick }: IPageLinkProps) {
    return (
        <a href={base} 
           className={className}
           onClick={(evt) => onClick && onClick(no, evt)}
           style={{ textDecoration: "none" }}>
            {no}
        </a>
    );
}

export default React.memo(PageLink);