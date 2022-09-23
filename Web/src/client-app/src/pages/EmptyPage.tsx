import React from "react";
import MainLayout from "../layouts/MainLayout";
import MainSection from "../components/MainSection";

function EmptyPage() {
    return (
        <MainLayout>
            <MainSection />
        </MainLayout>
    )
}

export default React.memo(EmptyPage);
