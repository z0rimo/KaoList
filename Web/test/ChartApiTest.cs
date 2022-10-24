// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Http.Json;
using System.Text.Json;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeRabbits.KaoList.Web.Test;

public class ChartApiTest
{
    private readonly WebApplicationFactory<Program> _application;

    public ChartApiTest()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.Single(
                        d => d.ServiceType == typeof(DbContextOptions<KaoListDataContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<KaoListDataContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<KaoListDataContext>();

                    db.Database.EnsureCreated();

                    Utilities.InitializeDbForTests(db);
                });
            });
    }

    [Fact]
    public async Task GetList()
    {
        var client = _application.CreateClient();

        var response = await client.GetAsync("/api/charts/list?part=id");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ChartResponse>();
        var answer = new ChartResponse
        {
            ETag = Guid.NewGuid().ToString(),
            Items = new List<ChartItem>
            {
                new ChartItem
                {
                    Id = "24f266f2-d34b-488c-a974-63f0a1671c3d",
                    ETag = "e3eb3a53-4a70-4501-bfac-99b292873495",
                },
                new ChartItem
                {
                    Id = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                    ETag = "61eee3da-3b9f-423b-a466-874d2a085c78",
                },
            }
        };

        Assert.Equal(JsonSerializer.Serialize(result!.Items), JsonSerializer.Serialize(answer.Items));
    }

    [Fact]
    public async Task GetListWithSnippet()
    {
        var client = _application.CreateClient();

        var response = await client.GetAsync("/api/charts/list?part=snippet");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ChartResponse>();
        var answer = new ChartResponse
        {
            ETag = Guid.NewGuid().ToString(),
            Items = new List<ChartItem>
            {
                new ChartItem
                {
                    Id = "24f266f2-d34b-488c-a974-63f0a1671c3d",
                    ETag = "e3eb3a53-4a70-4501-bfac-99b292873495",
                    Snippet = new ChartSnippet
                    {
                        Composer = new ChartUser
                        {
                            Id = "ca4dd1f0-36cb-4fe7-b4a1-4947659a8129",
                            NickName = "",
                        },
                        Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 31, millisecond: 919),
                        Singgers = new[] {
                            new ChartUser
                            {
                                Id="42239b25-df67-49c2-a028-d93016bff8e3",
                                NickName="木村世治",
                            },
                        },
                        Title = "felt",
                        Karaoke = new Dictionary<string, ChartKaraokeItem>(),
                    },
                },
                new ChartItem
                {
                    Id = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                    ETag = "61eee3da-3b9f-423b-a466-874d2a085c78",
                    Snippet = new ChartSnippet
                    {
                        Composer = new ChartUser
                        {
                            Id = "42239b25-df67-49c2-a028-d93016bff8e3",
                            NickName = "木村世治",
                        },
                        Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 32, millisecond: 283),
                        Singgers = new[] {
                            new ChartUser
                            {
                                Id = "00004e53-a27c-423e-892c-e5a54f665cee",
                                NickName = "木村世治(hurdy gurdy / Pale Green)",
                            },
                        },
                        Title = "bright lights",
                        Karaoke = new Dictionary<string, ChartKaraokeItem>(),
                    },
                },
            }
        };

        Assert.Equal(JsonSerializer.Serialize(result!.Items), JsonSerializer.Serialize(answer.Items));
    }
}
