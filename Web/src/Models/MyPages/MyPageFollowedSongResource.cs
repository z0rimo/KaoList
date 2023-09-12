// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageFollowedSongResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#followedSong";

        public string? Id { get; set; } = default!;

        public string? Title { get; set; }

        public DateTime? Created { get; set; }
    }
}
