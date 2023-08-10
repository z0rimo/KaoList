// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Songs;

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class DiscoverChartSnippet
    {
        public string? Thumbnail { get; set; } = default!;

        public string? Title { get; set; } = default!;

        public IEnumerable<ChartUser> SingUser { get; set; } = default!;

        public SongKaraokeItem? Karaoke { get; set; }
    }
}
