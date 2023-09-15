// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.
namespace CodeRabbits.KaoList.Song;

/// <summary>
/// A log of visits to the SongDetail page.
/// </summary>
public class SongDetailLog
{
    /// <summary>
    /// Id to identify the SongDetail Log.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();
      
    /// <summary>
    /// The time the SongDetail Log was created.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of the Sing that appeared on the SongDetail page.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// Id of the user who accessed the SongDetail page.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify the user who accessed the SongDetail page.
    /// </summary>
    public virtual string? IdentityToken { get; set; }
}
