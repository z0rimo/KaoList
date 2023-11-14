// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;

namespace CodeRabbits.KaoList.Web;

public class ClientRequestParametersProvider : IClientRequestParametersProvider
{
    public ClientRequestParametersProvider(
        IOptions<ApiAuthorizationOptions> options,
        IOptions<ForwardedHeadersOptions>? forwardedHeadersOptions = null)
    {
        Options = options;
        ForwardedHeadersOptions = forwardedHeadersOptions;
    }


    public IOptions<ApiAuthorizationOptions> Options { get; }

    public IOptions<ForwardedHeadersOptions>? ForwardedHeadersOptions { get; }

    protected virtual string GetScheme(HttpContext context)
    {
        var protoKey = ForwardedHeadersOptions?.Value.ForwardedProtoHeaderName;
        if (protoKey is not null && context.Request.Headers.TryGetValue(protoKey, out var scheme))
        {
            return scheme.Single() ?? context.Request.Scheme;
        }

        return context.Request.Scheme;
    }

    public IDictionary<string, string> GetClientParameters(HttpContext context, string clientId)
    {
        var client = Options.Value.Clients[clientId];
#pragma warning disable 0618
        // Deprecated in Identity Server 6.0
        var authority = context.GetIdentityServerIssuerUri();
#pragma warning restore 0618
        if (!client.Properties.TryGetValue(ApplicationProfilesPropertyNames.Profile, out var type))
        {
            throw new InvalidOperationException($"Can't determine the type for the client '{clientId}'");
        }

        string responseType;
        switch (type)
        {
            case ApplicationProfiles.IdentityServerSPA:
            case ApplicationProfiles.SPA:
            case ApplicationProfiles.NativeApp:
                responseType = "code";
                break;
            default:
                throw new InvalidOperationException($"Invalid application type '{type}' for '{clientId}'.");
        }

        return new Dictionary<string, string>
        {
            ["authority"] = authority,
            ["client_id"] = client.ClientId,
            ["redirect_uri"] = $"{GetScheme(context)}://{context.Request.Host.Value}{client.RedirectUris.First()}",
            ["post_logout_redirect_uri"] = $"{GetScheme(context)}://{context.Request.Host.Value}{client.PostLogoutRedirectUris.First()}",
            ["response_type"] = responseType,
            ["scope"] = string.Join(" ", client.AllowedScopes)
        };
    }
}
