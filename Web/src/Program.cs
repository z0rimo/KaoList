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

services.AddTransient<SongService>();
services.AddTransient<UserService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<KaoListUser, KaoListDataContext>();

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
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddRazorPages();
builder.Services.Configure<SvgTagHelperOption>(o =>
{
    o.BasePath = "./Svgs";
});

builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

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

}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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
