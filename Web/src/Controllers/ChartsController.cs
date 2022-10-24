// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ChartsController(KaoListDataContext context, IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<ChartItem> SingToChartItemAsync(Sing sing, bool includeSnippet)
        {
            using var scopre = _serviceScopeFactory.CreateScope();
            var context = scopre.ServiceProvider.GetRequiredService<KaoListDataContext>();
            var userManager = scopre.ServiceProvider.GetRequiredService<UserManager<KaoListUser>>();

            ChartSnippet? snippet = null;
            var inst = context.Instrumental.Where(inst => inst.Id == sing.InstrumentalId).First();
            if (includeSnippet)
            {
                var singgers = await Task.WhenAll(context.SingUsers.Where(us => us.SingId == sing.Id)
                    .ToArray()
                    .Select(async s => new ChartUser
                    {
                        Id = s.UserId!,
                        NickName = (await userManager.FindByIdAsync(s.UserId)).NickName!
                    }));

                snippet = new ChartSnippet
                {
                    Composer = new ChartUser
                    {
                        Id = inst.Composer!,
                        NickName = (await userManager.FindByIdAsync(inst.Composer)).NickName!
                    },
                    Created = sing.Created,
                    Karaoke = context.Karaokes.Where(karaoke => karaoke.SingId == sing.Id).ToDictionary(
                        karaoke => karaoke.Provider!,
                        karaoke => new ChartKaraokeItem { No = karaoke.No! }),
                    Singgers = singgers,
                    Title = inst.Title!
                };
            }

            return new ChartItem
            {
                ETag = inst.ConcurrencyStamp!,
                Id = sing.Id!,
                Snippet = snippet
            };
        }

        [HttpGet("list")]
        public async Task<ChartResponse> GetListAsync(string part, string? type, DateTime? startDate, DateTime? endDate, int maxResults = 5)
        {
            var parts = part.Split(",");
            IQueryable<Sing> sings = _context.Sings;
            if (startDate is not null)
            {
                sings = sings.Where(item => startDate <= item.Created);
            }

            if (endDate is not null)
            {
                sings = sings.Where(item => item.Created < endDate);
            }

            sings = sings.OrderBy(item => item.Id).Take(maxResults);

            var isSnippet = parts.Contains("snippet");
            var items = await Task.WhenAll(sings.ToArray().Select(async item => await SingToChartItemAsync(item, isSnippet)));

            return new ChartResponse
            {
                ETag = Guid.NewGuid().ToString(),
                Items = items
            };
        }
    }

    public class ChartUser
    {
        public string Id { get; set; } = default!;
        public string NickName { get; set; } = default!;
    }

    public class ChartKaraokeItem
    {
        public string No { get; set; } = default!;
    }

    public class ChartSnippet
    {
        public string Title { get; set; } = default!;
        public DateTime? Created { get; set; } = default!;
        public ChartUser Composer { get; set; } = default!;
        public IEnumerable<ChartUser> Singgers { get; set; } = default!;
        public string? SoundId { get; set; }
        public Dictionary<string, ChartKaraokeItem>? Karaoke { get; set; }
    }

    public record ChartItem
    {
        public string Id { get; set; } = default!;
        public string ETag { get; set; } = default!;
        public ChartSnippet? Snippet { get; set; } = default!;
    }

    public record ChartResponse
    {
        public string ETag { get; set; } = default!;
        public IEnumerable<ChartItem> Items { get; set; } = default!;
    }
}
