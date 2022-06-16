// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// Specifies the degree of protection of the playlist
/// </summary>
public class KaoListPlaylistPrivacyState
{
    /// <summary>
    /// The id indicating the protection status of the current playlist
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The name of the current protection status in the playlist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Localized name of the protection state currently applied to the playlist
    /// </summary>
    public string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that must change whenever a playlist is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
