// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Data;
using System.Globalization;
using CodeRabbits.KaoList.Web.Services.Mananas;
using CodeRabbits.KaoList.Identity;
using Microsoft.EntityFrameworkCore;
using CodeRabbits.KaoList.Web.Services.YouTubes;

namespace CodeRabbits.KaoList.Web.Services;

public class SongService
{
    private readonly KaoListDataContext _context;
    private readonly MananaService _mananaService;
    private readonly YouTubeService _youTubeService;

    public SongService(
        KaoListDataContext context,
        MananaService mananaService,
        YouTubeService youTubeService)
    {
        _context = context;
        _mananaService = mananaService;
        _youTubeService = youTubeService;
    }

    public void DeleteAll()
    {
        var allInstrumentals = _context.Instrumental.ToList();
        _context.Instrumental.RemoveRange(allInstrumentals);

        var allSings = _context.Sings.ToList();
        _context.Sings.RemoveRange(allSings);

        _context.SaveChanges();
    }

    public async Task FetchAndSaveSongsToDb()
    {
        var brands = Enum.GetValues(typeof(MananaBrand)).Cast<MananaBrand>();

        foreach (var brand in brands)
        {
            for (var i = 1; i <= 9; i++)
            {
                var mananaSongs = await _mananaService.GetSongsByNoAsync(i, brand);
                foreach (var mananaSong in mananaSongs)
                {
                    mananaSong.No = mananaSong.No.Trim();
                    mananaSong.Title = mananaSong.Title.Trim();
                    mananaSong.Singer = mananaSong.Singer.Trim();
                    mananaSong.Composer = mananaSong.Composer?.Trim();
                    mananaSong.Lyricist = mananaSong.Lyricist?.Trim();
                    await SaveMananaSongToDatabase(mananaSong);
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    private Karaoke? GetExistingKaraoke(string provider, string no)
    {
        return _context.Karaokes
            .AsNoTracking()
            .FirstOrDefault(k => k.Provider == provider && k.No == no);
    }

    private async Task SaveMananaSongToDatabase(MananaSong mananaSong)
    {
        var culture = new CultureInfo("ko-kr");
        var existingKaraoke = GetExistingKaraoke(mananaSong.Brand, mananaSong.No);
        if (existingKaraoke != null)
        {
            return;
        }

        var defaultDate = "0000-00-00";
        DateTime? releaseDate = DateTime.MinValue;
        if (mananaSong.Release.HasValue && mananaSong.Release.Value.ToString("yyyy-MM-dd", culture) != defaultDate)
        {
            releaseDate = mananaSong.Release.Value;
        }

        var instrumental = new Instrumental
        {
            Title = mananaSong.Title,
            NormalizedTitle = mananaSong.Title.ToUpper(),
            SoundId = null,
            Created = DateTime.UtcNow,
            Composer = mananaSong.Composer,
        };

        _context.Instrumental.Add(instrumental);

        var sing = new Sing
        {
            InstrumentalId = instrumental.Id,
            SoundId = null,
            Language = null,
            Created = releaseDate
        };

        _context.Sings.Add(sing);

        var userinfo = new KaoListUser
        {
            NickName = mananaSong.Singer,
            NormalizedNickName = mananaSong.Singer.ToUpper(),
            NickNameEditedDatetime = DateTime.UtcNow
        };

        _context.Users.Add(userinfo);

        var user = _context.Users.FirstOrDefault(u => u.NickName == mananaSong.Singer);

        if (user != null)
        {
            var singUser = new SingUser
            {
                SingId = sing.Id,
                UserId = user.Id
            };

            if (!_context.SingUsers.Any(su => su.SingId == singUser.SingId && su.UserId == singUser.UserId))
            {
                _context.SingUsers.Add(singUser);
            }
        }

        var karaoke = new Karaoke
        {
            Provider = mananaSong.Brand,
            Created = releaseDate,
            No = mananaSong.No,
            SingId = sing.Id
        };

        try
        {
            _context.Karaokes.Add(karaoke);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"내부 예외: {ex.InnerException.Message}");
            }
        }
    }

    public async Task<IEnumerable<(string SingId, string Query)>> GetYouTubeSearchDataAsync()
    {
        var singsWithTitlesAndInstrumentals = await _context.Sings
            .Join(_context.Instrumental,
                  sing => sing.InstrumentalId,
                  instrumental => instrumental.Id,
                  (sing, instrumental) => new { sing.Id, instrumental.Title, Instrumental = instrumental })
            .Where(x => x.Instrumental.SoundId == null)
            .ToListAsync();

        var searchQueries = new List<(string SingId, string Query)>();

        foreach (var item in singsWithTitlesAndInstrumentals)
        {
            var nickname = await _context.SingUsers
                .Where(su => su.SingId == item.Id)
                .Join(_context.Users,
                      su => su.UserId,
                      user => user.Id,
                      (su, user) => user.NickName)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(nickname))
            {
                var query = $"{item.Title} {nickname}";
                searchQueries.Add((item.Id ?? string.Empty, query ?? string.Empty));    
            }
        }

        return searchQueries;
    }

    public async Task<string?> CheckSoundIdAsync(string? singId)
    {
        var soundId = await (from sing in _context.Sings
                             join inst in _context.Instrumental on sing.InstrumentalId equals inst.Id
                             where sing.Id == singId
                             select inst.SoundId
                             )
                             .FirstOrDefaultAsync();

        return soundId;
    }

    public async Task UpdateSoundIdAsync(string instId, string title, string nickname)
    {
        var videoId = await _youTubeService.SearchSoundIdAsync(title, nickname);

        if (!string.IsNullOrEmpty(videoId))
        {
            var instrumental = _context.Instrumental.FirstOrDefault(i => i.Id == instId);
            if (instrumental != null)
            {
                instrumental.SoundId = videoId;
                _context.Update(instrumental);

                await _context.SaveChangesAsync();
            }
        }
    }

    /*public async Task UpdateSoundIdAsync1(IEnumerable<string> singIds)
    {
        foreach (var singId in singIds)
        {
            var song = _context.Sings.Include(s => s.in)
        }
    }*/
}
