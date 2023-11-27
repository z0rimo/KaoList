// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Http.Headers;

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeSearchService
{
    private readonly HttpClient _client;
    private readonly ILogger<YouTubeSearchService> _logger;

    public YouTubeSearchService(
        HttpClient client,
        ILogger<YouTubeSearchService> logger
        )
    {
        _client = client;
        _logger = logger;
    }

    public async Task<YouTubeSearchListResponse> GetSearchListAsync(
        YouTubeSearchOptions options,
        string? q,
        string? accessToken,
        string? apiKey
        )
    {
        var queryParams = new List<string>
        {
            $"part={options.Part.ToString().ToLower()}",
            $"q={q}",
            $"regionCode={options.RegionCode.ToString().Replace("_", "-")}",
            $"maxResults={options.MaxResults}",
            $"type={options.Type}"
        };

        if (!string.IsNullOrEmpty(apiKey))
        {
            queryParams.Add($"key={apiKey}");
        }

        if (!string.IsNullOrEmpty(accessToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var queryString = string.Join("&", queryParams);
        var url = $"https://www.googleapis.com/youtube/v3/search?{queryString}";

        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"YouTube API call failed: {response.StatusCode}");
            return null;
        }
    }
}
