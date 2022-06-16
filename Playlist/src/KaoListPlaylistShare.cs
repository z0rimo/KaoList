// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// Information for sharing playlists
/// </summary>
public class KaoListPlaylistShare
{
    /// <summary>
    /// The playlist unique key to share
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The user ID to be shared
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The role played by the shared recipient on the playlist
    /// </summary>
    public string? ShareRole { get; set; }
}
