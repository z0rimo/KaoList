// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class LikedChartListResponse : KaoListPageResponse
    {
        public override string Kind { get; set; } = "kaoList#likedChartListResponse";

        public IEnumerable<LikedChartResource>? resources { get; set; }
    }
}
