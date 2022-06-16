// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Xunit;

namespace CodeRabbits.KaoList.Playlist.Test;

public class KaoListPlaylistSingItemValidationTest
{
    public static readonly object?[][] CorrectData ={
            new object?[] { null, null, null },
            new object?[] { "en-US", "en-US", DateTime.MaxValue }
     };

    [Theory, MemberData(nameof(CorrectData))]
    public void TypeValidationTest(string? playListId, string? singId, DateTime? createTime)
    {
        var playListInstrumentalItem = new KaoListPlaylistSingItem
        {
            PlaylistId = playListId,
            SingId = singId,
            CreateTime = createTime
        };

        Assert.Equal(playListInstrumentalItem.PlaylistId, playListId);
        Assert.Equal(playListInstrumentalItem.SingId, singId);
        Assert.Equal(playListInstrumentalItem.CreateTime, createTime);
    }
}
