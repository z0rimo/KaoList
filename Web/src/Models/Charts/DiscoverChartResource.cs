// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class DiscoverChartResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#discoverChart";

        public DiscoverChartSnippet? Snippet { get; set; }
    }
}
