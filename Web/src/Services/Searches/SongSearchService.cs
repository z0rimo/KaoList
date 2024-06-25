// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Models.Searches;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using CodeRabbits.KaoList.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Utils;

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

    public async Task<SongSearchListResponse> SongSearchAsync(string query, int offset, int maxResults)
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new Exception("HttpContext is null");
        var token = httpContext.GetIdentityToken();
        var userId = _userManager.GetUserId(httpContext.User);

        var normalizedQuery = SongTitleNormalizeHelper.NormalizeQuery(query);

        var results = new List<Instrumental>();

        var exactMatches = await _context.Instrumental
            .Where(inst => inst.NormalizedTitle == normalizedQuery)
            .ToListAsync();
        results.AddRange(exactMatches);

        var prefixMatches = await _context.Instrumental
            .Where(inst => inst.NormalizedTitle!.StartsWith(normalizedQuery) && inst.NormalizedTitle != normalizedQuery)
            .ToListAsync();
        results.AddRange(prefixMatches);

        var containsMatches = await _context.Instrumental
            .Where(inst => inst.NormalizedTitle!.Contains(normalizedQuery) && !inst.NormalizedTitle.StartsWith(normalizedQuery))
            .ToListAsync();
        results.AddRange(containsMatches);

        var suffixMatches = await _context.Instrumental
            .Where(inst => inst.NormalizedTitle!.EndsWith(normalizedQuery) && !inst.NormalizedTitle.StartsWith(normalizedQuery) && !inst.NormalizedTitle.Contains(normalizedQuery))
            .ToListAsync();
        results.AddRange(suffixMatches);

        results = results.Distinct().ToList();
        var totalResults = results.Count;

        var instrumentals = results
            .Skip(offset)
            .Take(maxResults)
            .ToList();

        var resources = new List<SongSearchResource>();

        foreach (var inst in instrumentals)
        {
            var sings = await _context.Sings
                .Where(s => s.InstrumentalId == inst.Id)
                .ToListAsync();

            foreach (var sing in sings)
            {
                var isBlinded = await _context.SingBlinds
                    .AnyAsync(b => b.SingId == sing.Id && b.UserId == userId);

                if (isBlinded)
                {
                    continue;
                }

                var singUsers = await _context.SingUsers
                    .Where(su => su.SingId == sing.Id)
                    .Join(_context.Users, su => su.UserId, user => user.Id, (su, user) => new { su, user.NickName })
                    .ToListAsync();

                var artistNames = singUsers.Select(su => new SongUser
                {
                    Id = su.su.UserId,
                    Nickname = su.NickName
                }).ToList();

                var karaokeInfo = await _context.Karaokes
                    .Where(k => k.SingId == sing.Id)
                    .Select(k => new { k.Provider, k.No })
                    .FirstOrDefaultAsync();

                var isLiked = await _context.SingFollowers
                    .AnyAsync(f => f.SingId == sing.Id && f.UserId == userId);

                var thumbnail = await _context.Sounds
                    .Where(s => s.Id == inst.SoundId)
                    .Select(s => new ThumbnailResource
                    {
                        Url = s.Path,
                        Width = 480,
                        Height = 480
                    })
                    .FirstOrDefaultAsync() ?? new ThumbnailResource();

                await _logService.CreateSongSearchLogAsync(query, sing.Id, userId, token, inst.Title, string.Join(", ", artistNames.Select(a => a.Nickname)));

                var resource = new SongSearchResource
                {
                    Id = new SongSearchItem
                    {
                        Id = sing.Id
                    },
                    Etag = inst.ConcurrencyStamp,
                    Snippet = new SongSearchSnippet
                    {
                        SingId = sing.Id,
                        Created = sing.Created,
                        Title = inst.Title,
                        Artists = artistNames,
                        Thumbnail = thumbnail,
                        Karaokes = karaokeInfo == null ? null : new List<Karaoke> { new Karaoke
                    {
                        No = karaokeInfo.No,
                        Provider = karaokeInfo.Provider
                    }},
                        IsLiked = isLiked
                    }
                };

                resources.Add(resource);
            }
        }

        var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

        return new SongSearchListResponse
        {
            Etag = Guid.NewGuid().ToString(),
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
