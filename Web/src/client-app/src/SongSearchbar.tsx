import React from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import Searchbar from "./components/Searchbar";
import { useSearchContext } from "./contexts/SearchContext";
import RoutePath from "./RoutePath";

function SongSearchbar(props: React.HTMLAttributes<HTMLDivElement>) {
    const { t } = useTranslation("Searchbar");
    const { q, setQ } = useSearchContext();
    const navigate = useNavigate();

    const handleChange = React.useCallback<React.ChangeEventHandler<HTMLInputElement>>(
        (evt) => {
            setQ(evt.target.value);
        }, [setQ]
    );

    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>(
        (evt) => {
            evt.preventDefault();
            const formData = new FormData(evt.currentTarget);
            const data = formData.get('q');
            if (typeof data === 'string') {
                setQ(data);
                navigate(`${RoutePath['search']}?q=${data}&page=1`);
            }
        }, [setQ, navigate]
    );

    return (
        <Searchbar {...props}
            className="song-searchbar"
            value={q ?? ""}
            onSubmit={handleSubmit}
            onChange={handleChange}
            placeholder={t('Search Song') ?? "Search songs..."}
        />
    );
}

export default React.memo(SongSearchbar);