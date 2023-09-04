import React from "react";
import { useTranslation } from "react-i18next";
import Searchbar from "./components/Searchbar";
import { useSearchContext } from "./contexts/SearchContext";

interface ISongSearchbarProps {
    onSubmit?: React.FormEventHandler<HTMLFormElement>;
}

function SongSearchbar(props: ISongSearchbarProps) {
    const { t } = useTranslation("Searchbar");
    const { q, setQ } = useSearchContext();

    const handleChange = React.useCallback<React.ChangeEventHandler<HTMLInputElement>>
        (evt => {
            setQ(evt.target.value);
        }, [setQ]);

    return (
        <Searchbar
            className="song-searchbar"
            value={q!}
            onSubmit={props.onSubmit}
            onChange={handleChange}
            placeholder={t('Search Song') ?? undefined}
        />
    )
}

export default React.memo(SongSearchbar);