import React from "react";
import StringHelper from "../../StringHelper";
import ClassNameHelper from "../../ClassNameHelper";
import SearchButton from "../buttons/SearchButton";
import './Searchbar.scss';

interface SearchbarProps {
    className?: string;
    value: string | string[];
    placeholder?: string;
    onChange?: React.ChangeEventHandler<HTMLInputElement>;
    onSubmit?: React.FormEventHandler<HTMLFormElement>;
}

function Searchbar(props: SearchbarProps) {
    const { value, onSubmit, className } = props;
    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>
        (evt => {
            evt.preventDefault();
            if (StringHelper.isWhiteSpace(value.toString())) {
                return;
            }

            onSubmit && onSubmit(evt);
        }, [value, onSubmit]);

    return (
        <form className={ClassNameHelper.concat('searchbar', className)} onSubmit={handleSubmit}>
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