// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json.Serialization;
using CodeRabbits.AspNetCore.Authentication.Naver;
using CodeRabbits.AspNetCore.Authentication.Kakao;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web;
using CodeRabbits.KaoList.Web.Identitys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using CodeRabbits.Extensions.DependencyInjection;
using CodeRabbits.AspNetCore.Razor.TagHelpers;
using CodeRabbits.KaoList.Web.Services;
using CodeRabbits.KaoList.Web.Datas;
using System.Security.Claims;
using CodeRabbits.KaoList.Web.Areas.Identity.Pages;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using CodeRabbits.KaoList.Web.IdentityServer;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using CodeRabbits.KaoList.Web.Services.Mananas;
using System.Text.Json;
using CodeRabbits.KaoList.Web.Services.Songs;
using CodeRabbits.KaoList.Web.Services.YouTubes;
using Polly.Extensions.Http;
using Polly;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<KaoListDataContext>(options =>
    options.UseSqlServer(connectionString, b =>
    {
        b.MigrationsAssembly("CodeRabbits.KaoList.Web");

        b.EnableRetryOnFailure();
    }));

services.AddScoped<SongService>();
services.AddScoped<SongScoreService>();
services.AddScoped<MananaService>();
services.AddHostedService<DailyTaskService>();
services.AddScoped<UserService>();
services.AddScoped<LogService>();
services.AddScoped<NaverEmailSender>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<KaoListDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<KaoListUser>();
/*builder.Services.AddIdentity<KaoListUser, KaoListRole>()
    .AddSignInManager()
    .AddEntityFrameworkStores<KaoListDataContext>()
    .AddClaimsPrincipalFactory<KaoListUserClaimsPrincipalFactory<KaoListUser>>()
    .AddDefaultTokenProviders();*/
builder.Services.AddIdentityCore<KaoListUser>(options =>
    {
        options.Stores.MaxLengthForKeys = 128;

        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddSignInManager()
    .AddRoles<KaoListRole>()
    .AddEntityFrameworkStores<KaoListDataContext>()
    .AddClaimsPrincipalFactory<KaoListUserClaimsPrincipalFactory<KaoListUser>>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>();

builder.Services.AddIdentityServer(options =>
    {
        options.Authentication.CookieSameSiteMode = SameSiteMode.None;
    }).AddApiAuthorization<KaoListUser, KaoListDataContext>();

builder.Services.AddAuthentication(o =>
    {
        o.DefaultScheme = IdentityConstants.ApplicationScheme;
        o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddGoogle(go =>
    {
        go.ClientId = configuration.GetRequiredValue<string>(AuthenticationKey.GoogleClientId);
        go.ClientSecret = configuration.GetRequiredValue<string>(AuthenticationKey.GoogleClientSecret);
    })
    .AddKakao(ko =>
    {
        ko.ClientId = configuration.GetRequiredValue<string>(AuthenticationKey.KakaoClientId);
        ko.ClientSecret = configuration.GetRequiredValue<string>(AuthenticationKey.KakaoClientSecret);

        ko.ClaimActions.MapJsonThirdKey("nickname", "properties", "kakao_account", "nickname");
    })
    .AddNaver(no =>
    {
        no.ClientId = configuration.GetRequiredValue<string>(AuthenticationKey.NaverClientId);
        no.ClientSecret = configuration.GetRequiredValue<string>(AuthenticationKey.NaverClientClientSecret);

        no.ClaimActions.MapJsonSubKey("nickname", "response", "nickname");
    })
    .AddIdentityServerJwt()
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });


builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    });
builder.Services.AddRazorPages();
builder.Services.Configure<SvgTagHelperOption>(o =>
{
    o.BasePath = "./Svgs";
});

builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

builder.Services.AddScoped<IClientRequestParametersProvider, ClientRequestParametersProvider>();
builder.Services.AddScoped<IRedirectUriValidator, RedirectUriValidator>();

builder.Services.AddScoped<IServerUrls, ServerUrls>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddHttpClient<YouTubeService>(options =>
{
}).AddPolicyHandler(p =>
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
});

var app = builder.Build();
using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var dbContext = serviceProvider.GetRequiredService<KaoListDataContext>();

SeedData.Initialize(dbContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDefaultFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "scripts")),
        RequestPath = "/ts"
    });

    app.UseHttpsRedirection();
}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always,
});

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();

public partial class Program { }
