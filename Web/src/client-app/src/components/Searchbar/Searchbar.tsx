import React from "react";
import StringHelper from "../../StringHelper";
import LazySearchIcon from "../../svgs/LazySearchIcon";
import './Searchbar.scss';

interface SearchbarProps {
    value: string | string[];
    placeholder?: string;
    onChange?: React.ChangeEventHandler<HTMLInputElement>;
    onSubmit?: React.FormEventHandler<HTMLFormElement>;
}

const SearchButton = React.memo((props: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
    return (
        <button className="search-button" {...props}>
            <LazySearchIcon className="search-icon" />
        </button>
    )
});

function Searchbar(props: SearchbarProps) {
    const { value, onSubmit } = props;
    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>
        (evt => {
            evt.preventDefault();
            if (StringHelper.isWhiteSpace(value.toString())) {
                return;
            }

            onSubmit && onSubmit(evt);
        }, [value, onSubmit]);

    return (
        <form className="searchbar" onSubmit={handleSubmit}>
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