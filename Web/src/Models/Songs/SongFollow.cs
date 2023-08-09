// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongFollow
    {
        public string? SongId { get; set; }

        public string? UserId { get; set; }

        public DateTime? Created { get; set; }
    }
}
