// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Song;

/// <summary>
/// This is the log information for searching for a song.
/// </summary>
public class SongSearchLog
{
    /// <summary>
    /// A unique id in the search log.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The query used to search.
    /// </summary>
    public virtual string? Query { get; set; }

    /// <summary>
    /// Sing's Id from the search results.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The user who made the search.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify the user who search it.
    /// </summary>
    public virtual string? IdentityToken { get; set; }

    /// <summary>
    /// It is the date of search.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;
}
