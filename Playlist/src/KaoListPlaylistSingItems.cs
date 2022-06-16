// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// The playlist information about the user who sang the song and song
/// </summary>
public class KaoListPlaylistSingItem
{
    /// <summary>
    /// The playlist unique key
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The id of song added to playlist
    /// </summary>
    public string? SingId { get; set; }

    /// <summary>
    /// The date added to playlist
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}