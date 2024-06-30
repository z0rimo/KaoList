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

        var results = await (from inst in _context.Instrumental
                             join sing in _context.Sings on inst.Id equals sing.InstrumentalId
                             join su in _context.SingUsers on sing.Id equals su.SingId
                             join user in _context.Users on su.UserId equals user.Id
                             join k in _context.Karaokes on sing.Id equals k.SingId into karaokeGroup
                             from kg in karaokeGroup.DefaultIfEmpty()
                             join s in _context.Sounds on inst.SoundId equals s.Id into soundGroup
                             from sg in soundGroup.DefaultIfEmpty()
                             join sf in _context.SingFollowers on new { sing.Id, UserId = userId } equals new { Id = sf.SingId, sf.UserId } into followerGroup
                             from fg in followerGroup.DefaultIfEmpty()
                             where inst.NormalizedTitle == normalizedQuery
                                || inst.NormalizedTitle!.StartsWith(normalizedQuery) && inst.NormalizedTitle != normalizedQuery
                                || inst.NormalizedTitle!.EndsWith(normalizedQuery) && !inst.NormalizedTitle.StartsWith(normalizedQuery)
                             select new
                             {
                                 Instrumental = inst,
                                 Sing = sing,
                                 User = user,
                                 Karaoke = kg,
                                 Sound = sg,
                                 IsLiked = fg != null

                             }).ToListAsync();

        var totalResults = results.Select(r => r.Instrumental).Distinct().Count();

        var paginatedResults = results.Skip(offset).Take(maxResults).ToList();

        var resources = new List<SongSearchResource>();

        foreach (var group in paginatedResults.GroupBy(r => r.Sing.Id))
        {
            var first = group.First();
            var artistNames = group.Select(g => new SongUser
            {
                Id = g.User.Id,
                Nickname = g.User.NickName
            }).ToList();

            var resource = new SongSearchResource
            {
                Id = new SongSearchItem
                {
                    Id = first.Sing.Id
                },
                Etag = first.Instrumental.ConcurrencyStamp,
                Snippet = new SongSearchSnippet
                {
                    SingId = first.Sing.Id,
                    Created = first.Sing.Created,
                    Title = first.Instrumental.Title,
                    Artists = artistNames,
                    Thumbnail = first.Sound == null ? null : new ThumbnailResource
                    {
                        Url = first.Sound.Path,
                        Width = 480,
                        Height = 480
                    },
                    Karaokes = group.Where(g => g.Karaoke != null).Select(g => new Karaoke
                    {
                        No = g.Karaoke.No,
                        Provider = g.Karaoke.Provider
                    }).ToList(),
                    IsLiked = first.IsLiked
                }
            };

            resources.Add(resource);
        }

        var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

        return new SongSearchListResponse
        {
            Etag = Guid.NewGuid().ToString(),
            Items = resources,
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
