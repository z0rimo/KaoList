import React, { Component } from 'react';

function NavMenu(props: React.HTMLAttributes<HTMLDivElement>) {
    return (
        <div {...props} />
    )
}

export default React.memo(NavMenu);