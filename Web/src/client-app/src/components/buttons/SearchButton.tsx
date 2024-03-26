import React from "react";
import ClassNameHelper from "../../ClassNameHelper";
import LazySearchIcon from "../../svgs/LazySearchIcon";

function AdvertiserButton({ className, ...rest }: React.ButtonHTMLAttributes<HTMLButtonElement>) {
  return (
    <button className={ClassNameHelper.concat("search-button", className)} {...rest}>
      <LazySearchIcon className="search-icon" />
    </button>
  )
}

export default React.memo(AdvertiserButton);