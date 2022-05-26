namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// Blinded user
/// </summary>
public class KaoListUserBlind
{
    /// <summary>
    /// Id of the user who requested to be blocked.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Id of the blinded user.
    /// </summary>
    public string? BlinedUserId { get; set; }

    /// <summary>
    /// Blind date.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}
