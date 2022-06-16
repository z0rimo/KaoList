// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class YouTubePlaylistShareValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null },
            new object?[] { "en-US", "en-US"}
     };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? youTubePlaylistId, string? userId)
    {
        var youTubePlaylistShared = new YouTubePlaylistShared
        {
            YouTubePlaylistId = youTubePlaylistId,
            UserId = userId,
        };

        Assert.Equal(youTubePlaylistShared.YouTubePlaylistId, youTubePlaylistId);
        Assert.Equal(youTubePlaylistShared.UserId, userId);
    }
}
