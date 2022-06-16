// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistLocalizedValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null, null },
            new object?[] { "en-US", "en-US", "en-US", "max" }
     };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? playListId, string? i18nName, string? name, string? concurrencyStamp)
    {
        var playListLocalizeds = new KaoListPlaylistLocalized
        {
            PlaylistId = playListId,
            ConcurrencyStamp = concurrencyStamp,
            Name = name,
            I18nName = i18nName
        };

        Assert.Equal(playListLocalizeds.PlaylistId, playListId);
        Assert.Equal(playListLocalizeds.ConcurrencyStamp, concurrencyStamp);
        Assert.Equal(playListLocalizeds.Name, name);
        Assert.Equal(playListLocalizeds.I18nName, i18nName);
    }
}
