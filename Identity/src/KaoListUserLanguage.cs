using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// Set the user's default language
/// </summary>
public class KaoListUserLanguage
{
    /// <summary>
    /// The user id in the language setting
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// set language
    /// </summary>
    public string? I18nName { get; set; }
}
