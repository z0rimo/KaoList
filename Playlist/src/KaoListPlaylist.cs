// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// Playlists for saving and linking with others
/// </summary>
public class KaoListPlaylist
{
    /// <summary>
    /// The playlist unique key
    /// </summary>
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The user created playlist name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The status of whether the playlist is visible
    /// </summary>
    public string? PrivacyStatus { get; set; }

    /// <summary>
    /// Playlist owner Id
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// A random value that must change whenever a playlist is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
