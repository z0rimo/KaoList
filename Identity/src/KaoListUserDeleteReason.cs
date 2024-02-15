// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// The reason the user wrote when they delete.
/// </summary>
public class KaoListUserDeleteReason
{
    /// <summary>
    /// Unique id of the reason for deletion.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Id of the user who deleted the account.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The last modified date of the nickname.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// The date deleted by user.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}
