// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Models.Searches;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using CodeRabbits.KaoList.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeRabbits.KaoList.Song;

namespace CodeRabbits.KaoList.Web.Services.Searches;

public class SongSearchService : ISearchService
{
    private readonly KaoListDataContext _context;
    private readonly LogService _logService;
    private readonly UserManager<KaoListUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SongSearchService(
        KaoListDataContext context,
        LogService logService,
        UserManager<KaoListUser> userManager,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _logService = logService;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    private string NormalizeQuery(string query)
    {
        var normalizedQuery = new StringBuilder();
        foreach (var c in query)
        {
            if (c >= '가' && c <= '힣')
            {
                normalizedQuery.Append(c);
            }
            else
            {
                normalizedQuery.Append(char.ToLowerInvariant(c));
            }
        }

        return normalizedQuery.ToString().Normalize(NormalizationForm.FormC);
    }

    public async Task<SearchListResponse> SearchAsync(IEnumerable<string> queries, int offset, int maxResults)
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new Exception("HttpContext is null");
        var token = httpContext.GetIdentityToken();
        var userId = _userManager.GetUserId(httpContext.User);
        var normalizedQueries = queries.Select(NormalizeQuery).ToArray();

        var results = new List<Instrumental>();

        foreach (var query in normalizedQueries)
        {
            // 완전 일치
            var exactMatches = await _context.Instrumental
                .Where(inst => inst.NormalizedTitle == query)
                .ToListAsync();
            results.AddRange(exactMatches);

            // 전방 일치
            var prefixMatches = await _context.Instrumental
                .Where(inst => inst.NormalizedTitle!.StartsWith(query) && inst.NormalizedTitle != query)
                .ToListAsync();
            results.AddRange(prefixMatches);

            // 부분 일치
            var containsMatches = await _context.Instrumental
                .Where(inst => inst.NormalizedTitle!.Contains(query) && !inst.NormalizedTitle.StartsWith(query))
                .ToListAsync();
            results.AddRange(containsMatches);

            // 후방 일치
            var suffixMatches = await _context.Instrumental
                .Where(inst => inst.NormalizedTitle!.EndsWith(query) && !inst.NormalizedTitle.StartsWith(query) && !inst.NormalizedTitle.Contains(query))
                .ToListAsync();
            results.AddRange(suffixMatches);
        }

        results = results.Distinct().ToList();
        var totalResults = results.Count;

        var instrumentals = results
            .Skip(offset)
            .Take(maxResults)
            .ToList();

        var resources = new List<SearchResource>();

        foreach (var inst in instrumentals)
        {
            var sings = await _context.Sings
                .Where(s => s.InstrumentalId == inst.Id)
                .ToListAsync();

            foreach (var sing in sings)
            {
                var singUsers = await _context.SingUsers
                    .Where(su => su.SingId == sing.Id)
                    .Join(_context.Users, su => su.UserId, user => user.Id, (su, user) => new { su, user.NickName })
                    .ToListAsync();

                var artistNames = singUsers.Select(su => su.NickName).ToList();
                var artistName = string.Join(", ", artistNames);

                var karaokeInfo = await _context.Karaokes
                    .Where(k => k.SingId == sing.Id)
                    .Select(k => new { k.Provider, k.No })
                    .FirstOrDefaultAsync();

                await _logService.CreateSongSearchLogAsync(string.Join(" ", queries), sing.Id, userId, token, inst.Title, artistName);

                var resource = new SearchResource
                {
                    Id = new SearchSong
                    {
                        Id = sing.Id
                    },
                    Etag = inst.ConcurrencyStamp,
                    Snippet = new SongSnippet
                    {
                        Created = sing.Created,
                        Title = inst.Title,
                        SongUsers = singUsers.Select(su => new SongUser
                        {
                            Id = su.su.UserId,
                            Nickname = su.NickName
                        }).ToList(),
                        Composer = inst.Composer,
                        Thumbnail = inst.SoundId == null ? null : new ThumbnailResource
                        {
                            Url = inst.SoundId,
                            Width = 300,
                            Height = 300
                        },
                        Karaoke = karaokeInfo == null ? null : new SongKaraokeItem
                        {
                            No = karaokeInfo.No,
                            ProviderName = karaokeInfo.Provider
                        }
                    }
                };

                resources.Add(resource);
            }
        }

        var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

        return new SearchListResponse
        {
            Etag = new Guid().ToString(),
            Items = resources.Distinct().ToList(),
            NextPageToken = nextPageToken,
            PrevPageToken = prevPageToken,
            PageInfo = new PageInfo
            {
                TotalResults = totalResults,
                ResultPerPage = resources.Count
            }
        };
    }
}
