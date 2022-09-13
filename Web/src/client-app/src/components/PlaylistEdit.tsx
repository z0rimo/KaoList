import React from "react";
import { useTranslation } from "react-i18next";
import NotImplementedError from "../errors/NotImplementedError";
import LazyMinusIcon from "../svgs/LazyMinusIcon";
import LazyPlusIcon from "../svgs/LazyPlusIcon";

function PlaylistEdit() {
    const { t } = useTranslation('PlaylistEdit');

    const handlePlusButtonClick = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    const handleRemoveButtonClick = React.useCallback(() => {
        throw new NotImplementedError();
    }, []);

    return (
        <div className="edit">
            <div className="edit-text">
                <button className="btn" onClick={handlePlusButtonClick}>
                    <p>{t('Add playlist')}</p>
                    <LazyPlusIcon className="plus-icon" />
                </button>
                <button className="btn" onClick={handleRemoveButtonClick}>
                    <p>{t('Remove playlist')}</p>
                    <LazyMinusIcon className="minus-icon" />
                </button>
            </div>
        </div>
    )
}

export default React.memo(PlaylistEdit);