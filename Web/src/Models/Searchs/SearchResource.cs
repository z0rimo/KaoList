// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Songs;

namespace CodeRabbits.KaoList.Web.Models.Searchs
{
    public class SongSearchResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#searchResult";

        public SearchSong? Id { get; set; } = default!;

        public SongSnippet? Snippet { get; set; }
    }
}
