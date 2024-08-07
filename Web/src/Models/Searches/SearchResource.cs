// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.
using CodeRabbits.KaoList.Web.Models.Songs;

namespace CodeRabbits.KaoList.Web.Models.Searches
{
    public class SearchResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#songSearchResult";

        public SongSearchItem? Id { get; set; } = default!;

        public SongSnippet? Snippet { get; set; }
    }
}
