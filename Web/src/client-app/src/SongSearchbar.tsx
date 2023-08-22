import React from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import Searchbar from "./components/Searchbar";
import { useSearchContext } from "./contexts/SearchContext";
import RoutePath from "./RoutePath";

function SongSearchbar() {
    const { t } = useTranslation("Searchbar");
    const { q, setQ } = useSearchContext();
    const navigate = useNavigate();
    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>
        (() => {
            navigate(`${RoutePath['search']}?q=${q}`);
        }, [navigate, q])

    const handleChange = React.useCallback<React.ChangeEventHandler<HTMLInputElement>>
        (evt => {
            setQ(evt.target.value);
        }, [setQ]);

    return (
        <Searchbar
            value={q!}
            onSubmit={handleSubmit}
            onChange={handleChange}
            placeholder={t('Search Song') ?? undefined}
        />
    )
}

export default React.memo(SongSearchbar);