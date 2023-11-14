// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace CodeRabbits.KaoList.Web;

public class CodeRabbitsDiscoveryResponseGenerator : DiscoveryResponseGenerator
{
    public CodeRabbitsDiscoveryResponseGenerator(
        IdentityServerOptions options,
        IResourceStore resourceStore,
        IKeyMaterialService keys,
        ExtensionGrantValidator extensionGrants,
        ISecretsListParser secretParsers,
        IResourceOwnerPasswordValidator resourceOwnerValidator,
        IOptions<ForwardedHeadersOptions>? forwardedHeadersOptions,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DiscoveryResponseGenerator> logger) : base(
            options,
            resourceStore,
            keys,
            extensionGrants,
            secretParsers,
            resourceOwnerValidator,
            logger)
    {
        ForwardedHeadersOptions = forwardedHeadersOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    protected readonly IHttpContextAccessor _httpContextAccessor;

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

    /// <summary>
    /// Creates the discovery document.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="issuerUri">The issuer URI.</param>
    public override async Task<Dictionary<string, object>> CreateDiscoveryDocumentAsync(string baseUrl, string issuerUri)
    {
        var entries = await base.CreateDiscoveryDocumentAsync(baseUrl, issuerUri);
        var httpCotnext = _httpContextAccessor.HttpContext;
        if (httpCotnext is null)
        {
            return entries;
        }

        var originScheme = httpCotnext.Request.Scheme;
        var scheme = GetScheme(httpCotnext);
        if (scheme == originScheme)
        {
            return entries;
        }


        var regex = new Regex(Regex.Escape(originScheme));
        entries = UpdateScheme(originScheme, scheme, entries);

        return entries;

        Dictionary<string, object> UpdateScheme(string originScheme, string scheme, Dictionary<string, object> obj)
        {
            foreach (var (key, value) in obj)
            {
                if (value is string str)
                {
                    if (str.StartsWith(originScheme) && !str.StartsWith(scheme))
                    {
                        obj[key] = regex!.Replace(str, scheme, 1);
                    }
                }
                else if (value is Dictionary<string, object> dict)
                {
                    obj[key] = UpdateScheme(originScheme, scheme, dict);
                }
            }

            return obj;
        }
    }


}
