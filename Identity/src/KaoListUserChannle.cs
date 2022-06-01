namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// KaoListUserChannle on other sites owned by the user
/// </summary>
public class KaoListUserChannel
{
    /// <summary>
    /// The name of the provider that provides the channel to users.
    /// </summary>
    public string? ChannelProvider { get; set; }

    /// <summary>
    /// The key to access the channel.
    /// </summary>
    public string? ProviderKey { get; set; }

    /// <summary>
    /// Id of the user who owns the channel.
    /// </summary>
    public string? UserId { get; set; }
}
