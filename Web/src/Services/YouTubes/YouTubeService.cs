// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json;

namespace CodeRabbits.KaoList.Web.Services.YouTubes;

public class YouTubeService
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public YouTubeService(
        HttpClient client,
        string apiKey
        )
    {
        _client = client;
        _apiKey = apiKey;
    }

    public async Task<string?> SearchSoundIdAsync(string title, string nickname)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(nickname))
        {
            return null;
        }

        var query = $"{title} {nickname}";
        var searchOptions = new YouTubeSearchOptions
        {
            MaxResults = 1,
        };
        var searchResponse = await GetSearchListAsync(searchOptions, query, _apiKey);

        var videoId = searchResponse?.Items?.FirstOrDefault()?.Id?.VideoId;

        if (videoId is null)
        {
            return null;
        }

        return videoId;
    }

    public async Task<YouTubeSearchListResponse> GetSearchListAsync(
        YouTubeSearchOptions options,
        string? q,
        string? apiKey
        )
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Converters = { new DateTimeJsonConverter() },
            PropertyNameCaseInsensitive = true
        };

        var queryParams = new List<string>
        {
            $"part={options.Part?.ToString().ToLower()}",
            $"q={q}",
            $"regionCode={options.RegionCode}",
            $"maxResults={options.MaxResults}",
            $"type={options.Type}"
        };

        if (!string.IsNullOrEmpty(apiKey))
        {
            queryParams.Add($"key={apiKey}");
        }

        var queryString = string.Join("&", queryParams);
        var url = $"https://www.googleapis.com/youtube/v3/search?{queryString}";

        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"YouTube API call failed with status code: {response.StatusCode}");
        }

        var searchResponse = await response.Content.ReadFromJsonAsync<YouTubeSearchListResponse>(jsonOptions);

        return searchResponse is null ? throw new Exception("YouTube API returned empty response.") : searchResponse;
    }
}
