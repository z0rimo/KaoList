using CodeRabbits.KaoList.Playlist;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Data;

public partial class KaoListDataContext
{
    public virtual DbSet<KaoListPlaylist> Playlists { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistLocalized> PlaylistLocalizeds { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistShare> PlaylistShares { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistShareRole> PlaylistShareRoles { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistSingItem> PlaylistSingItems { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistPrivacyState> PlaylistPrivacyStates { get; set; } = default!;
    public virtual DbSet<KaoListPlaylistPlayLog> PlaylistPlayLogs { get; set; } = default!;
    public virtual DbSet<YouTubePlaylistShared> YouTubePlaylistShares { get; set; } = default!;
    public virtual DbSet<YouTubePlaylistSyncInfo> YouTubePlaylistSyncInfos { get; set; } = default!;
}