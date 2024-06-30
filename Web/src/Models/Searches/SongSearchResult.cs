// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;

namespace CodeRabbits.KaoList.Web.Models.Searches;

public class SongSearchResult
{
    public Instrumental? Instrumental { get; set; }

    public Sing? Sing { get; set; }

    public SingUser? SingUser { get; set; }

    public Karaoke? Karaoke { get; set; }

    public bool IsLiked { get; set; }

    public Sound? Sound { get; set; }

    public int MatchType { get; set; }
}
