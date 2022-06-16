// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Playlist;

/// <summary>
/// The information of users who will be shared with YouTube playlists
/// </summary>
public class YouTubePlaylistShared
{
    /// <summary>
    /// The id of the playlist to get from YouTube
    /// </summary>
    public string? YouTubePlaylistId { get; set; }

    /// <summary>
    /// The id of the user to share the playlist with
    /// </summary>
    public string? UserId { get; set; }
}