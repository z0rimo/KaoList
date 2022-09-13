import { PlaylistSharedRoleType } from "../PlaylistSharedRole";
import ISong from "./ISong";

interface IPlaylist {
    id: string,
    title: string,
    sharedRole: PlaylistSharedRoleType,
    viewCount: number,
    sharedCount: number,
    createdTime: Date,
    songs: ISong[];
}

export default IPlaylist;