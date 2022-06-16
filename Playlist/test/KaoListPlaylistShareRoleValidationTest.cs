// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistShareRoleValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null, null },
            new object?[] { "en-US", "en-US", "en-US", "en-US" }
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? id, string? name, string? normalizedName, string? concurrencyStamp)
    {
        var playListShareRoles = new KaoListPlaylistShareRole
        {
            Id = id,
            Name = name,
            NormalizedName = normalizedName,
            ConcurrencyStamp = concurrencyStamp,
        };

        Assert.Equal(playListShareRoles.Id, id);
        Assert.Equal(playListShareRoles.Name, name);
        Assert.Equal(playListShareRoles.NormalizedName, normalizedName);
        Assert.Equal(playListShareRoles.ConcurrencyStamp, concurrencyStamp);
    }
}
