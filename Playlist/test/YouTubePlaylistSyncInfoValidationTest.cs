// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class YouTubePlaylistSyncInfoValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null },
            new object?[] { "en-US", "en-US"}
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? playListId, string? youTubePlaylistId)
    {
        var youTubePlaylistSyncs = new YouTubePlaylistSyncInfo
        {
            PlaylistId = playListId,
            YouTubePlaylistId = youTubePlaylistId,
        };

        Assert.Equal(youTubePlaylistSyncs.PlaylistId, playListId);
        Assert.Equal(youTubePlaylistSyncs.YouTubePlaylistId, youTubePlaylistId);
    }
}
