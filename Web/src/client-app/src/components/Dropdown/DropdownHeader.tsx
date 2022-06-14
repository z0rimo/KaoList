import React from "react";

type DropdownToggleProps = {
  children: React.ReactElement;
};

function DropdownHeader(props: DropdownToggleProps): React.ReactElement | null {
  const childElement = React.Children.only(props.children);
  return React.cloneElement(
    childElement,
  );
}

export default React.memo(DropdownHeader);
