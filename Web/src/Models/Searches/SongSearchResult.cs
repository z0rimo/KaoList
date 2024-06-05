// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Searches;

public class SongSearchResult
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string? SongSearchId { get; set; }

    public string? SingId { get; set; }
}
