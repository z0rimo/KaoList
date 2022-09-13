export type PlaylistSharedRoleType = 'Unlisted' | 'Private';

type RoleObjectType = { [key in string]: PlaylistSharedRoleType };

interface IPlaylistSharedRole extends RoleObjectType {
    private: 'Private',
    unlisted: 'Unlisted'
}

const PlaylistSharedRole = Object.freeze<IPlaylistSharedRole>({
    private: 'Private',
    unlisted: 'Unlisted'
});

export default PlaylistSharedRole;