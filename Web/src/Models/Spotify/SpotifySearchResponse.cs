// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Models.Spotify
{
    public class SpotifySearchResponse
    {
        [JsonPropertyName("albums")]
        public SpotifyAlbums? Albums { get; set; }
    }
}
