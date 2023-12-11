// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeSearchOptions
{
    public YouTubePart? Part { get; set; } = YouTubePart.Id;

    public string? Q { get; set; }

    public YouTubeRegionCode? RegionCode { get; set; } = YouTubeRegionCode.KR;

    public int? MaxResults { get; set; } = 5;

    public YouTubeSearchType? Type { get; set; } = YouTubeSearchType.Video;
}
