// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// The information of youtube playlists to be shared
/// </summary>
public class YouTubePlaylistSyncInfo
{
    /// <summary>
    /// The playlist unique dictionary key
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The id of the playlist to get from YouTube
    /// </summary>
    public string? YouTubePlaylistId { get; set; }
}