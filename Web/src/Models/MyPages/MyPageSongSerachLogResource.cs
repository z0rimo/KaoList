// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageSongSerachLogResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#songSerachLog";

        public string? Id { get; set; } = default!;

        public string? Query { get; set; }

        public DateTime? Created { get; set; }
    }
}
