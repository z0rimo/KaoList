import React from "react";
import FooterMenu from "./FooterMenu";
import FooterInfo from "./FooterInfo";
import { useLocation } from "react-router-dom";
import PathHelper from "../../PathHelper";

function useFooterClassName() {
    const location = useLocation();
    return React.useMemo(() => {
        if (PathHelper.getAreaNameByPath(location.pathname) === 'root') {
            return 'lime-theme';
        }
        else {
            return undefined;
        }
    }, [location.pathname]); 
}

function Footer() {
    const footerClassName = useFooterClassName();

    return (
        <footer className={footerClassName}>
            <div className="footer-content-region">
                <FooterMenu />
                <FooterInfo /> 
            </div>
        </footer>
    )
}

export default React.memo(Footer);