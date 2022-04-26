// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

using Microsoft.AspNetCore.Authentication.OAuth;

namespace CodeRabbits.Kakao;

/// <summary>
/// <see cref="AuthenticationProperties"/> for a Kakao OAuth challenge.
/// </summary>
public class KakaoChallengeProperties : OAuthChallengeProperties
{
    /// <summary>
    /// The parameter key for the "prompt" argument being used for a challenge request.
    /// </summary>
    public static readonly string PromptParameterKey = "prompt";


    /// <summary>
    /// The parameter key for the "nonce" argument being used for a challenge request.
    /// </summary>
    public static readonly string NonceParameterKey = "nonce";

    /// <summary>
    /// Initializes a new instance of <see cref="KakaoChallengeProperties"/>.
    /// </summary>
    public KakaoChallengeProperties()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="KakaoChallengeProperties"/>.
    /// </summary>
    /// <inheritdoc />
    public KakaoChallengeProperties(IDictionary<string, string?> items)
        : base(items)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="KakaoChallengeProperties"/>.
    /// </summary>
    /// <inheritdoc />
    public KakaoChallengeProperties(IDictionary<string, string?> items, IDictionary<string, object?> parameters)
        : base(items, parameters)
    { }

    /// <summary>
    /// The "prompt" parameter value being used for a challenge request.
    /// </summary>
    public string? Prompt
    {
        get => GetParameter<string>(PromptParameterKey);
        set => SetParameter(PromptParameterKey, value);
    }

    /// <summary>
    /// No fixed format string used to prevent token replay attacks 
    /// when issuing ID tokens together via OpenID Connect with a challenge request.
    /// </summary>
    public string? Nonce
    {
        get => GetParameter<string>(NonceParameterKey);
        set => SetParameter(PromptParameterKey, value);
    }
}