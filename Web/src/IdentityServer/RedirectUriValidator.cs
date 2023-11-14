// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace CodeRabbits.KaoList.Web.IdentityServer;

public class RedirectUriValidator : IRedirectUriValidator
{
    /// <summary>
    /// Checks if a given URI string is in a collection of strings (using ordinal ignore case comparison)
    /// </summary>
    /// <param name="uris">The uris.</param>
    /// <param name="requestedUri">The requested URI.</param>
    /// <returns></returns>
    protected bool StringCollectionContainsString(IEnumerable<string> uris, string requestedUri)
    {
        if (uris.IsNullOrEmpty()) return false;        

        return uris.Contains(new Uri(requestedUri).PathAndQuery, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether a redirect URI is valid for a client.
    /// </summary>
    /// <param name="requestedUri">The requested URI.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <c>true</c> is the URI is valid; <c>false</c> otherwise.
    /// </returns>
    public virtual Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {
        return Task.FromResult(StringCollectionContainsString(client.RedirectUris, requestedUri));
    }

    /// <summary>
    /// Determines whether a post logout URI is valid for a client.
    /// </summary>
    /// <param name="requestedUri">The requested URI.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <c>true</c> is the URI is valid; <c>false</c> otherwise.
    /// </returns>
    public virtual Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
    {
        return Task.FromResult(StringCollectionContainsString(client.PostLogoutRedirectUris, requestedUri));
    }
}
