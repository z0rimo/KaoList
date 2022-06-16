// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistPlayLogValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null, null, null },
            new object?[] { int.MinValue, "en-US", "en-US", "en-US", DateTime.MaxValue},
     };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(int? id, string? playListId, string? userId, string? identityToken, DateTime? createtime)
    {
        var playListPlayLogs = new KaoListPlaylistPlayLog
        {
            Id = id,
            PlaylistId = playListId,
            UserId = userId,
            IdentityToken = identityToken,
            CreateTime = createtime
        };

        Assert.Equal(playListPlayLogs.Id, id);
        Assert.Equal(playListPlayLogs.PlaylistId, playListId);
        Assert.Equal(playListPlayLogs.UserId, userId);
        Assert.Equal(playListPlayLogs.IdentityToken, identityToken);
        Assert.Equal(playListPlayLogs.CreateTime, createtime);
    }
}
