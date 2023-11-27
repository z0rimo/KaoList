// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeSearchResource
{
    public string Kind = "youtube#serachResult";

    public string? ETag { get; set; }

    public YouTubeSearchId? Id { get; set; }

    public YouTubeSearchSnippet? Snippet { get; set; }
}
