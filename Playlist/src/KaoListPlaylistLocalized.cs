// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// The localization of text in playlists according to region
/// </summary>
public class KaoListPlaylistLocalized
{
    /// <summary>
    /// The id of language currently being applied.
    /// </summary>
    public string? I18nName { get; set; }

    /// <summary>
    /// The localized playlist names.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Id to identify each playlist
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// A random value that must change whenever a playlist is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
