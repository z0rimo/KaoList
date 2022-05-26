namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// User follow
/// </summary>
public class KaoListUserFollower
{
    /// <summary>
    /// User id of the follower.
    /// </summary>
    public string? FollwerUserId { get; set; }

    /// <summary>
    /// User id of the follow.
    /// </summary>
    public string? FollowUserId { get; set; }

    /// <summary>
    /// The date you started following.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}
