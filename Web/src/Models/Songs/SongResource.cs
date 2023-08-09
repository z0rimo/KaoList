// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#song";

        public string? Id { get; set; } = default!;

        public SongSnippet? Snippet { get; set; }

        public SongStatistics? Statistics { get; set; }
    }
}
