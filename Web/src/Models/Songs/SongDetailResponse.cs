// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongDetailResponse : KaoListPageResponse
    {
        public override string Kind { get; set; } = "kaoList#songDetailResponse";

        public SongResource? Item { get; set; }

        public IEnumerable<SongResource>? OtherSongs { get; set; }

        public IEnumerable<SongResource>? OtherMySongs { get; set; }
    }
}
