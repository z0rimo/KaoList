// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace CodeRabbits.KaoList.Web.Test;

public static class Utilities
{
    public static async Task LoginAsync(HttpClient client, string email, string password)
    {
        var clinetId = Assembly.GetAssembly(typeof(Program))!.GetName().Name!;

        var response = await client.GetAsync($"/_configuration/{clinetId}");
        var oAuth2Config = await response.Content.ReadFromJsonAsync<IDictionary<string, string>>();

        var redirectUri = HttpUtility.UrlEncode(oAuth2Config["redirect_uri"]);
        var scope = HttpUtility.UrlEncode(oAuth2Config["scope"]);
        var codeVerifier = "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646";
        var codeVerifierBytes = Encoding.ASCII.GetBytes(codeVerifier);
        var hashedBytes = codeVerifierBytes.Sha256(
);
        var code_challenge = Base64Url.Encode(hashedBytes);
        response = await client.GetAsync($"/connect/authorize?client_id={clinetId}&redirect_uri={redirectUri}&response_type={oAuth2Config["response_type"]}&scope={scope}&state=c878ad85922d4689ab1aafae68cdfa0b&code_challenge={code_challenge}&code_challenge_method=S256&response_mode=query");

        var loginUri = response.RequestMessage.RequestUri;
        var loginHtml = await response.Content.ReadAsStringAsync();
        loginHtml = loginHtml.Replace("&copy;", "©");
        var xdocu = XDocument.Parse(loginHtml);
        var verificationTokenNode = xdocu.XPathSelectElement("//input[@name=\"__RequestVerificationToken\"]");
        var verificationToken = verificationTokenNode?.Attribute("value")?.Value;
        Assert.NotNull(verificationToken);

        var loginForm = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("Input.Email", email),
            new KeyValuePair<string, string>("Input.Password", password),
            new KeyValuePair<string, string>("RememberMe", "false"),
            new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken!)
        });

        response = await client.PostAsync(loginUri, loginForm);
        var queries = HttpUtility.ParseQueryString(response.RequestMessage!.RequestUri!.Query);
        var authorizationCode = queries.Get("code");
        Assert.NotNull(authorizationCode);
        var tokenForm = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("client_id", clinetId),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", authorizationCode!),
            new KeyValuePair<string, string>("code_verifier", codeVerifier),
            new KeyValuePair<string, string>("redirect_uri", oAuth2Config["redirect_uri"]),
        });
        response = await client.PostAsync("/connect/token", tokenForm);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>()!;
        var accessToken = result["access_token"].ToString();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }


    public static void InitializeDbForTests(KaoListDataContext db)
    {
        db.SaveChanges();
    }

    public static void ReinitializeDbForTests(KaoListDataContext db)
    {
        db.SaveChanges();

        InitializeDbForTests(db);
    }

    public static List<KaoListUser> GetKaoListUsers() => new()
    {
        new KaoListUser
        {
            Id = "00004e53-a27c-423e-892c-e5a54f665cee",
            NickName = "木村世治(hurdy gurdy / Pale Green)",
            NormalizedNickName = "木村世治(HURDY GURDY / PALE GREEN)",
            Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 27),
            UserName = "97b39e0e-a114-4482-bdf3-15ce7c959c96",
            NormalizedUserName = "97B39E0E-A114-4482-BDF3-15CE7C959C96",
            EmailConfirmed = false,
            SecurityStamp = "25592c5e-fff7-4194-93f9-ecb48fabe02a",
            ConcurrencyStamp = "7ec6487d-e2be-43ed-9a28-fadeb90c48aa",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
        },
        new KaoListUser
        {
            Id = "42239b25-df67-49c2-a028-d93016bff8e3",
            NickName = "木村世治",
            NormalizedNickName = "木村世治",
            Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 26, millisecond: 298),
            UserName = "44f1867d-bc20-48db-b7a7-627f33471531",
            NormalizedUserName = "44F1867D-BC20-48DB-B7A7-627F33471531",
            EmailConfirmed = false,
            SecurityStamp = "8761a834-4fa1-4fed-88cf-273dc3cedbd9",
            ConcurrencyStamp = "6f62f178-e5a1-4cef-951a-5f0c872192e0",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
        },
        new KaoListUser
        {
            Id="ca4dd1f0-36cb-4fe7-b4a1-4947659a8129",
            NickName="",
            NormalizedNickName="",
            Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 25, millisecond: 404),
            UserName = "0fc30f21-73bd-436a-9cd2-61d94bc31606",
            NormalizedUserName = "0FC30F21-73BD-436A-9CD2-61D94BC31606",
            EmailConfirmed = false,
            SecurityStamp = "92dab372-ef8b-4a14-849b-8d1b4709f14e",
            ConcurrencyStamp = "8d9c727c-35c1-44e9-9313-3c0f6f2044d7",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
        },
        new KaoListUser
        {
            Id="00000000-0000-0000-0000-000000000000",
            NickName="Test user",
            NormalizedNickName="TEST USER",
            Created = new DateTime(2022, 10, 31),
            UserName = "test@kaolist.net",
            NormalizedUserName = "TEST@KAOLIST.NET",
            EmailConfirmed = true,
            SecurityStamp = "GZECNHQ7QYDA5Z2NAZ43EQA2NOU5GTMV",
            ConcurrencyStamp = "b5076182-fe21-4db3-8eaf-2145627c74c4",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
            Email = "test@kaolist.net",
            PasswordHash = "AQAAAAEAACcQAAAAEA6pX8czGsTsg7XOTNKsUwDqq7a+dCZINAUCMNHdpwBu+DAZ0fRmAAsuC7EagqOLvw=="
        }
    };

    public static List<Sing> GetSings() => new()
    {
        new Sing
        {
            Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 32, millisecond: 283),
            Id="4df36b28-31ba-4e41-87aa-6d9309e1fc55",
            InstrumentalId="cd973782-c032-4e14-be1e-72bb32026eea"
        },
        new Sing
        {
            Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 31, millisecond: 919),
            Id="24f266f2-d34b-488c-a974-63f0a1671c3d",
            InstrumentalId="d950653d-847a-4707-9e57-ca51914c7c02"
        },
    };

    public static List<SingUser> GetSingUsers() => new()
    {
         new SingUser
        {
            SingId = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
            UserId = "00004e53-a27c-423e-892c-e5a54f665cee",
        },
        new SingUser
        {
            SingId = "24f266f2-d34b-488c-a974-63f0a1671c3d",
            UserId = "42239b25-df67-49c2-a028-d93016bff8e3",
        },
    };

    public static List<Instrumental> GetInstrumentals() => new()
    {
        new Instrumental
        {
            Id="cd973782-c032-4e14-be1e-72bb32026eea",
            Composer="42239b25-df67-49c2-a028-d93016bff8e3",
            ConcurrencyStamp="61eee3da-3b9f-423b-a466-874d2a085c78",
            Created=new DateTime(year: 2012, month: 11, day: 24),
            NormalizedTitle="bright lights",
            Title="bright lights",
        },
        new Instrumental
        {
            Id="d950653d-847a-4707-9e57-ca51914c7c02",
            Composer="ca4dd1f0-36cb-4fe7-b4a1-4947659a8129",
            ConcurrencyStamp="e3eb3a53-4a70-4501-bfac-99b292873495",
            Created=new DateTime(year: 2015, month: 2, day: 1),
            NormalizedTitle="felt",
            Title="felt",
        },
    };
}
