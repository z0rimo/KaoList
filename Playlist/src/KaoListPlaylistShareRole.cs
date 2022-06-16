// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// Specify in which protection state the playlist will be shared
/// </summary>
public class KaoListPlaylistShareRole
{
    /// <summary>
    /// The id of rule applied when the playlist is shared.
    /// </summary>
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The name of the rule applied when the playlist is shared
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Localized name of rule applied when playlist is shared
    /// </summary>
    public string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that must change whenever a playlist is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

