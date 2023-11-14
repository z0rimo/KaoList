// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Duende.IdentityServer.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace CodeRabbits.KaoList.Web.IdentityServer;

internal static class Constants
{
    public static class EnvironmentKeys
    {
        public const string IdentityServerBasePath = "idsvr:IdentityServerBasePath";
        public const string SignOutCalled = "idsvr:IdentityServerSignOutCalled";
    }
}

public class ServerUrls : IServerUrls
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// ctor
    /// </summary>
    public ServerUrls(
        IHttpContextAccessor httpContextAccessor,
        IOptions<ForwardedHeadersOptions>? forwardedHeadersOptions = null)
    {
        _httpContextAccessor = httpContextAccessor;
        ForwardedHeadersOptions = forwardedHeadersOptions;
    }
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

    /// <inheritdoc/>
    public string Origin
    {
        get
        {
            var context = _httpContextAccessor.HttpContext!;
            return GetScheme(context) + "://" + context.Request.Host.ToUriComponent();
        }
        set
        {
            var split = value.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

            var request = _httpContextAccessor.HttpContext.Request;
            request.Scheme = split.First();
            request.Host = new HostString(split.Last());
        }
    }

    /// <inheritdoc/>
    public string BasePath
    {
        get
        {
            return (_httpContextAccessor.HttpContext!.Items[Constants.EnvironmentKeys.IdentityServerBasePath] as string)!;
        }
        set
        {
            var url = value;
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            _httpContextAccessor.HttpContext!.Items[Constants.EnvironmentKeys.IdentityServerBasePath] = url;
        }
    }
}
