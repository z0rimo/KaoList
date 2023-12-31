// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class DiscoverChartResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#discoverChart";

        public string? Id { get; set; } = default!;

        public ChartSnippet? Snippet { get; set; }
    }
}
