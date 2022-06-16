// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// Stores information related to the playlist log.
/// </summary>
public class KaoListPlaylistPlayLog
{
    /// <summary>
    /// A unique number assigned to each log record in ascending order whenever a playlist log is created.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// A token to identify each user
    /// </summary>
    public string? IdentityToken { get; set; }

    /// <summary>
    /// Id to identify each playlist
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The user ID who played the playlist
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The time that playlist was played
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}
