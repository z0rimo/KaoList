// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongGetRatingResource
    {
        public string? Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SongRating Rating { get; set; }
    }
}
