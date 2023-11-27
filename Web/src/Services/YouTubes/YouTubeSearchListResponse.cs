// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models;

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeSearchListResponse
{
    public string Kind = "youtube#searchListResponse";

    public string? ETag { get; set; }

    public string? NextPageToken { get; set; }

    public string? PrevPageToken { get; set; }

    public string? RegionCode { get; set; }

    public PageInfo? PageInfo { get; set; }

    public IEnumerable<YouTubeSearchResource?>? Items { get; set; }
}
