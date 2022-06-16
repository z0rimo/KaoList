// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistShareValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null },
            new object?[] { "en-US", "en-US", "en-US" }
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? playListId, string? userId, string? shareRole)
    {
        var playListShares = new KaoListPlaylistShare
        {
            PlaylistId = playListId,
            UserId = userId,
            ShareRole = shareRole,
        };

        Assert.Equal(playListShares.PlaylistId, playListId);
        Assert.Equal(playListShares.UserId, userId);
        Assert.Equal(playListShares.ShareRole, shareRole);
    }
}
