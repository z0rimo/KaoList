import React from "react";
import ClassNameHelper from "../../ClassNameHelper";
import DropdownContent from "./DropdownContent";
import DropdownHeader from "./DropdownHeader";

function Dropdown(props: React.HTMLAttributes<HTMLDivElement>) {
  const { className, ...rest } = props;

  const classNames = ClassNameHelper.concat('dropdown', className);

  return (
    <div className={classNames} {...rest} />
  )
}

export default Object.assign(Dropdown, {
  Header: DropdownHeader,
  Content: DropdownContent,
});
