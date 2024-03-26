import React from "react";

function Table(props: React.DetailedHTMLProps<React.TableHTMLAttributes<HTMLTableElement>, HTMLTableElement>) {
    return <table {...props} />;
}

export default Table;