// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.
using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Models.Spotify
{
    public class SpotifyAlbum
    {
        [JsonPropertyName("images")]
        public List<SpotifyAlbumImage>? Images { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
