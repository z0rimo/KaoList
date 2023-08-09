// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongListResponse : KaoListPageResponse
    {
        public override string Kind { get; set; } = "kaoList#songListResponse";

        public IEnumerable<SongResource>? Items { get; set; }
    }
}
