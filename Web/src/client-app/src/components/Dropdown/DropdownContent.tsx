import React from "react";
import ClassNameHelper from "../../ClassNameHelper";

type DropdownContentProps = {
  children: React.ReactElement;
};


function DropdownContent(props: DropdownContentProps): React.ReactElement | null {
  const childElement = React.Children.only(props.children);

  return React.cloneElement(
    childElement, {
    className: ClassNameHelper.concat(childElement.props?.className, 'dropdown-content')
  });
}

export default React.memo(DropdownContent);
