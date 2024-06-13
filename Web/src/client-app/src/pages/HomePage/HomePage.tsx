import React from "react";
import MainLayout from "../../layouts/MainLayout";
import MainSection from "../../components/MainSection";
import LazyLogo from "../../svgs/LazyLogo";
import SongSearchbar from "../../SongSearchbar";
import ChartSection from "../../sections/home/ChartSection";
import './HomePage.scss';

function HomePage({
    className,
    ...rest
}: React.HTMLAttributes<HTMLDivElement>) {
    return (
        <MainLayout className="aquamarine-theme" {...rest}>
            <MainSection className="homepage-region">
                <div className="main-logo-wrapper">
                    <LazyLogo className="main-logo" />
                </div>
                <SongSearchbar className="song-searchbar" />
                <ChartSection />
            </MainSection>
        </MainLayout>
    )
}

export default React.memo(HomePage);