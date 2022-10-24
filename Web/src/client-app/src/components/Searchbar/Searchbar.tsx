import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import RoutePath from "../../RoutePath";
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

function Searchbar() {
    const { t } = useTranslation("Searchbar");
    const [q, setQ] = useState<string>('');
    const navigate = useNavigate();

    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>(evt => {
        evt.preventDefault();

        if (StringHelper.isWhiteSpace(q)) {
            return;
        }

        navigate(`${RoutePath['search']}?q=${q}`);
    }, [q])

    const handleChange = React.useCallback<React.ChangeEventHandler<HTMLInputElement>>(evt => {
        setQ(evt.target.value);
    }, []);

    return (
        <form className="searchbar" onSubmit={handleSubmit}>
            <input className="input"
                type="text"
                name="q"
                value={q}
                onChange={handleChange}
                placeholder={t('Search Song')}
                autoComplete="on"
            />
            <SearchButton />
        </form>
    )
}

export default React.memo(Searchbar);
