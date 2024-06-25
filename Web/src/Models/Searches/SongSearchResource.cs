// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Searches
{
    public class SongSearchResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#songSearchResult";

        public SongSearchItem? Id { get; set; } = default!;

        public SongSearchSnippet? Snippet { get; set; }
    }
}
