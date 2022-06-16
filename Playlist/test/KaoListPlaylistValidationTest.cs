// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null, null, null },
            new object?[] { "en-US", "en-US", "en-US", "en-US", "dfbda069-a85c-413f-a870-3dbb666367a6" },
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? id, string? userId, string? name, string? privacyStatus, string? concurrencyStamp)
    {
        var playLists = new KaoListPlaylist
        {
            Id = id,
            ConcurrencyStamp = concurrencyStamp,
            Name = name,
            UserId = userId,
            PrivacyStatus = privacyStatus
        };

        Assert.Equal(playLists.Id, id);
        Assert.Equal(playLists.ConcurrencyStamp, concurrencyStamp);
        Assert.Equal(playLists.Name, name);
        Assert.Equal(playLists.UserId, userId);
        Assert.Equal(playLists.PrivacyStatus, privacyStatus);
    }
}
