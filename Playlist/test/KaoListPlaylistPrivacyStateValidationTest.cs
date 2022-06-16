// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistPrivacyStateValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null, null },
            new object?[] { "en-US", "en-US", "en-US", "en-US" }
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? id, string? name, string? normalizedName, string? concurrencyStamp)
    {
        var playListPrivacyStates = new KaoListPlaylistPrivacyState
        {
            Id = id,
            Name = name,
            NormalizedName = normalizedName,
            ConcurrencyStamp = concurrencyStamp,
        };

        Assert.Equal(playListPrivacyStates.Id, id);
        Assert.Equal(playListPrivacyStates.Name, name);
        Assert.Equal(playListPrivacyStates.NormalizedName, normalizedName);
        Assert.Equal(playListPrivacyStates.ConcurrencyStamp, concurrencyStamp);
    }
}
