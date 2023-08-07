import React from "react";
import ClassNameHelper from "../../ClassNameHelper";
import StringHelper from "../../StringHelper";
import LazySearchIcon from "../../svgs/LazySearchIcon";
import './Searchbar.scss';
const SearchButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
    return (
        <button className="search-button" {...props}>
            <LazySearchIcon className="search-icon" />
        </button>
    )
});

interface ISearchbarProps {
    value: string;
    className?: string;
    placeholder?: string;
    onChange?: React.ChangeEventHandler<HTMLInputElement>;
    onSubmit?: React.FormEventHandler<HTMLFormElement>;
}

function Searchbar(props: ISearchbarProps) {
    const { value, onSubmit, className } = props;
    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>(evt => {
        evt.preventDefault();
        if (StringHelper.isWhiteSpace(value)) {
            return;
        }
        onSubmit && onSubmit(evt);
    }, [value, onSubmit]);

    return (
        <form className={ClassNameHelper.concat("searchbar", className)} onSubmit={handleSubmit}>
            <input className="input"
                type="text"
                name="q"
                value={props.value}
                onChange={props.onChange}
                placeholder={props.placeholder}
                autoComplete="on"
            />
            <SearchButton />
        </form>
    )
}
export default React.memo(Searchbar);