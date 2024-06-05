// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Http.Headers;
using System.Text.Json;
using CodeRabbits.KaoList.Web.Models.Spotify;

namespace CodeRabbits.KaoList.Web.Services;

public class SpotifyService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private string? _accessToken;
    private DateTime _tokenExpiration;

    public SpotifyService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration[AuthenticationKey.SpotifyClientId]
                    ?? throw new ArgumentNullException(nameof(configuration), "ClientId is not configured");
        _clientSecret = configuration[AuthenticationKey.SpotifyClientSecret]
                        ?? throw new ArgumentNullException(nameof(configuration), "ClientSecret is not configured");
    }

    private async Task<string> GetSpotifyTokenAsync()
    {
        if (_accessToken != null && DateTime.UtcNow < _tokenExpiration)
        {
            return _accessToken;
        }

        var authToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

        var postData = new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") };
        var content = new FormUrlEncodedContent(postData);

        var response = await _httpClient.PostAsync("https://accounts.spotify.com/api/token", content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(responseBody)
            ?? throw new InvalidOperationException("Failed to deserialize token response.");

        if (string.IsNullOrEmpty(tokenResponse.AccessToken))
        {
            throw new InvalidOperationException("Access token is null or empty.");
        }

        _accessToken = tokenResponse.AccessToken;
        _tokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

        return _accessToken;
    }

    public async Task<string?> GetTopTrackAlbumImageAsync(string query)
    {
        var token = await GetSpotifyTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(query)}&type=track&market=KR&limit=5&offset=0");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var searchResponse = JsonSerializer.Deserialize<SpotifyTrackSearchResponse>(responseBody);

        if (searchResponse?.Tracks?.Items == null || !searchResponse.Tracks.Items.Any())
        {
            return null;
        }

        var track = searchResponse.Tracks.Items.FirstOrDefault();
        return track?.Album?.Images?.FirstOrDefault()?.Url;
    }
}
