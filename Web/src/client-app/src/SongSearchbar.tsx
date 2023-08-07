import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import ClassNameHelper from "./ClassNameHelper";
import Searchbar from "./components/Searchbar";
import RoutePath from "./RoutePath";

interface ISongSearchbarProps {
    className?: string;
}

function SongSearchbar(props: ISongSearchbarProps) {
    const { className } = props;
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
        <Searchbar className={ClassNameHelper.concat("song-searchbar", className)}
            value={q}
            onSubmit={handleSubmit}
            onChange={handleChange}
            placeholder={t('Search Song') ?? undefined}
        />
    )
}
export default React.memo(SongSearchbar);