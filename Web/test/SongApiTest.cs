// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Http.Json;
using System.Text.Json;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CodeRabbits.KaoList.Web.Test;

public class SongApiTest
{
    private readonly WebApplicationFactory<Program> _application;
    private readonly HttpClient _client;

    public SongApiTest()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices();
            });
        _client = _application.CreateClient();
    }

    [Fact]
    public async Task GetListById()
    {
        var response = await _client.GetAsync("/api/songs/list?part=id&id=24f266f2-d34b-488c-a974-63f0a1671c3d&id=4df36b28-31ba-4e41-87aa-6d9309e1fc55");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<SongListResponse>();
        var answer = new SongListResponse
        {
            Etag = Guid.NewGuid().ToString(),
            resources = new List<SongResource>
            {
                new SongResource
                {
                    Id = "24f266f2-d34b-488c-a974-63f0a1671c3d",
                    Etag = "e3eb3a53-4a70-4501-bfac-99b292873495",
                },
                new SongResource
                {
                    Id = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                    Etag = "61eee3da-3b9f-423b-a466-874d2a085c78",
                },
            }
        };

        Assert.Equal(JsonSerializer.Serialize(result!.resources), JsonSerializer.Serialize(answer.resources));
    }

    [Fact]
    public async Task GetListBySnippet()
    {
        var response = await _client.GetAsync("/api/songs/list?part=snippet&id=24f266f2-d34b-488c-a974-63f0a1671c3d&id=4df36b28-31ba-4e41-87aa-6d9309e1fc55");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<SongListResponse>();
        var answer = new SongListResponse
        {
            Etag = Guid.NewGuid().ToString(),
            resources = new List<SongResource>
            {
                new SongResource
                {
                    Id = "24f266f2-d34b-488c-a974-63f0a1671c3d",
                    Etag = "e3eb3a53-4a70-4501-bfac-99b292873495",
                    Snippet = new SongSnippet
                    {
                        Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 31, millisecond: 919),
                        Title = "felt",
                        SongUsers = new List<SongUser>
                        {
                            new SongUser
                            {
                                Id = "42239b25-df67-49c2-a028-d93016bff8e3",
                                Nickname = "木村世治",
                            }
                        },
                        Thumbnail = new ThumbnailResource
                        {
                            Url = null,
                            Width = 300,
                            Height = 300,
                        },
                        Karaoke = new SongKaraokeItem
                        {
                            No = null,
                            ProviderName = null,
                        }
                    }
                },
                new SongResource
                {
                    Id = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                    Etag = "61eee3da-3b9f-423b-a466-874d2a085c78",
                    Snippet = new SongSnippet
                    {
                        Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 32, millisecond: 283),
                        Title = "bright lights",
                        SongUsers = new List<SongUser>
                        {
                            new SongUser
                            {
                                Id = "00004e53-a27c-423e-892c-e5a54f665cee",
                                Nickname = "木村世治(hurdy gurdy / Pale Green)",
                            }
                        },
                        Thumbnail = new ThumbnailResource
                        {
                            Url = null,
                            Width = 300,
                            Height = 300,
                        },
                        Karaoke = new SongKaraokeItem
                        {
                            No = null,
                            ProviderName = null,
                        }
                    }
                },
            }
        };

        Assert.Equal(JsonSerializer.Serialize(result!.resources), JsonSerializer.Serialize(answer.resources));
    }
}
