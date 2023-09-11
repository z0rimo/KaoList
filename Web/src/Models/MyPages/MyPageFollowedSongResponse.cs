// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageFollowedSongResponse : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#myPageFollowedSongResponse";

        public IEnumerable<MyPageFollowedSongResource>? items { get; set; }
    }
}
