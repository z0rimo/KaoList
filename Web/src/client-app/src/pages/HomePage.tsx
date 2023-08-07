import React from "react";
import MainLayout from "../layouts/MainLayout";
import MainSection from "../components/MainSection";
import DiscoverChart, { IDiscoverChartItem } from "../DiscoverChart";

const DiscoverCharItem = React.memo((props: IDiscoverChartItem) => {
    return (
        <tr>
            <td>{props.title}</td>
            <td>{props.composer?.nickName}</td>
            <td>{props.singgers?.map(item => item.nickName).join(", ")}</td>
            <td>{props.karaoke && props.karaoke['tj']?.no}</td>
            <td>{props.karaoke && props.karaoke['ky']?.no}</td>
        </tr>
    )
});

const discoverCharItemRender = (item: IDiscoverChartItem) => {
    return <DiscoverCharItem {...item} />
}

function HomePage() {
    return (
        <MainLayout className="aquamarine-theme">
            <MainSection>
                <DiscoverChart maxResults={50}
                    Table={props => <table {...props} />}
                    renderer={discoverCharItemRender}
                    thead={
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Composer</th>
                                <th>Singgers</th>
                                <th>Tj</th>
                                <th>Ky</th>
                            </tr>
                        </thead>
                    }
                />
            </MainSection>
        </MainLayout>
    )
}

export default React.memo(HomePage);
