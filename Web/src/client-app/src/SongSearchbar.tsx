import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import Searchbar from "./components/Searchbar";
import RoutePath from "./RoutePath";

function SongSearchbar() {
    const { t } = useTranslation("Searchbar");
    const [q, setQ] = useState<string>('');
    const navigate = useNavigate();
    const handleSubmit = React.useCallback<React.FormEventHandler<HTMLFormElement>>(() => {        
        navigate(`${RoutePath['search']}?q=${q}`);
    }, [q])

    const handleChange = React.useCallback<React.ChangeEventHandler<HTMLInputElement>>(evt => {
        setQ(evt.target.value);
    }, []);

    return (
        <Searchbar
            value={q}
            onSubmit={handleSubmit}
            onChange={handleChange}
            placeholder={t('Search Song') ?? undefined}
        />
    )
}

export default React.memo(SongSearchbar);