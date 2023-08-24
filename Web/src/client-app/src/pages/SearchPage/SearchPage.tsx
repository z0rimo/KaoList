import React from "react";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import SongSearchList, { ISongSearchListItem } from "../../SongSearchList";
import Pagination from "../../components/Pagination";

const SongSearchListItem = React.memo((props: ISongSearchListItem) => {
    let tjNo = "-";
    let kumyoungNo = "-";

    if (props.karaoke?.providerName === "tj") {
        tjNo = props.karaoke.no ?? "-";
    } else if (props.karaoke?.providerName === "kumyoung") {
        kumyoungNo = props.karaoke.no ?? "-";
    }

    return (
        <tr>
            <td>{props.thumbnail?.url}</td>
            <td>
                <p>{props.title}</p>
                <p>{props.songUsers?.map(item => item.nickname).join(", ")}</p>
            </td>
            <td>{tjNo}</td>
            <td>{kumyoungNo}</td>
            {/* <td>{props.liked}</td> */}
        </tr>
    )
})

const songSearchListItemRender = (item: ISongSearchListItem) => {
    return <SongSearchListItem {...item} />
}

const Table = React.memo((props: React.HTMLAttributes<HTMLTableElement>) => <table {...props} />);

function SearchPage() {
    return (
        <MainLayout>
            <MainSection>
                <SongSearchList
                    maxResults={20}
                    Table={Table}
                    renderer={songSearchListItemRender}
                    thead={
                        <thead>
                            <tr>
                                <th>Thumbnail</th>
                                <th>Title / SongUser</th>
                                <th>TJ</th>
                                <th>KY</th>
                                <th>Liked</th>
                            </tr>
                        </thead>
                    }
                />
                <Pagination />
            </MainSection>
        </MainLayout >
    )
}

export default React.memo(SearchPage);