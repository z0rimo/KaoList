// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeSearchSnippet
{
    public DateTime? PublishedAt { get; set; }

    public string? ChannelId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public IEnumerable<ThumbnailResource?>? Thumbnails { get; set; }

    public string? ChannelTitle { get; set; }

    public YouTubeLiveBroadcastContent? LiveBroadcastContent { get; set; }
}
