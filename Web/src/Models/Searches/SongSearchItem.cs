// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Searches
{
    public class SongSearchItem : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#song";

        public string? Id { get; set; }
    }
}
