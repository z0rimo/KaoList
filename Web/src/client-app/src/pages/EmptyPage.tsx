import React from "react";
import MainLayout from "../layouts/MainLayout";
import Page from "./Page";

function EmptyPage() {
    return (
        <MainLayout>
            <Page />
        </MainLayout>
    )
}

export default React.memo(EmptyPage);