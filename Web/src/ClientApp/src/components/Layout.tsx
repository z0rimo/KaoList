import React, { Component } from 'react';
import NavMenu from './NavMenu';

function Layout(props: React.HTMLAttributes<HTMLDivElement>) {
  const { children, ...rest } = props;

  return (
    <div {...rest}>
      <NavMenu />
    </div>
  )
}

export default React.memo(Layout);