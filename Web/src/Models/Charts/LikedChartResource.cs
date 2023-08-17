// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class LikedChartResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#likedChart";

        public string? Id { get; set; } = default!;

        public LikedChartSnippet? Snippet { get; set; }
    }
}
