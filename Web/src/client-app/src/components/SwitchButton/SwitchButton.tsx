import React from "react";
import './SwitchButton.scss';

const OffToggle = React.memo(() => {
    return (
        <p className="toggle-text fs-9 off-active">OFF</p>
    )
});

const OnToggle = React.memo(() => {
    return (
        <p className="toggle-text fs-9 on-active">ON</p>
    )
});

function SwitchButton() {
    const [toggle, setToggle] = React.useState(false);
    const handleToggle = React.useCallback(() => {
        setToggle(!toggle)
    }, [toggle])

    return (
        <label className="switch-button">
            <input type="checkbox" />
            <span className="slider round" onClick={handleToggle}>
                {toggle ? <OnToggle /> : <OffToggle />}
            </span>
        </label>
    )
}

export default React.memo(SwitchButton);