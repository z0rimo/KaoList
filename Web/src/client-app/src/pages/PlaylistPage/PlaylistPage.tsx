import React from "react";
import PlaylistEdit from "../../components/PlaylistEdit";
import PlaylistTitle from "../../components/PlaylistTitle";
import Page from "../Page";
import IPlaylist from "../../models/IPlaylist";
import PlaylistSharedRole from "../../PlaylistSharedRole";
import MainLayout from "../../layouts/MainLayout";
import Playlist from "../../components/Playlist/Playlist";
import './PlaylistPage.scss';

const songs = [
    {
        id: '1',
        imgUrl: "https://i.ytimg.com/vi/zJQTIKOqtVc/hq720.jpg?sqp=-oaymwEcCOgCEMoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLCZm5Feimccl6NPn82RCt7ap_Tvmg",
        title: 'title',
        artist: 'artist',
    }
]

function PlaylistPage() {
    const [playlists] = React.useState<IPlaylist[]>([
        {
            id: '1',
            title: '이 지금 나는 살아있다',
            sharedRole: PlaylistSharedRole.unlisted,
            viewCount: 123,
            sharedCount: 12,
            createdTime: new Date(),
            songs: songs
        }
    ]);

    return (
        <MainLayout>
            <Page>
                <div className="playlist-area content-box-white">
                    <PlaylistTitle />
                    <div>
                        {playlists.map(item => <Playlist key={item.id} {...item} />)}
                    </div>
                    <PlaylistEdit />
                </div>
            </Page>
        </MainLayout>
    );
}

export default React.memo(PlaylistPage);