// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Data;
using System.Globalization;
using CodeRabbits.KaoList.Web.Services.Mananas;
using CodeRabbits.KaoList.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Services;

public class SongService
{
    private readonly KaoListDataContext _context;
    private readonly MananaService _mananaService;

    public SongService(
        KaoListDataContext context,
        MananaService mananaService)
    {
        _context = context;
        _mananaService = mananaService;
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
                    SaveMananaSongToDatabase(mananaSong);
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    private void SaveMananaSongToDatabase(MananaSong mananaSong)
    {
        var culture = new CultureInfo("ko-kr");
        var existingKaraoke = _context.Karaokes
            .FirstOrDefault(k => k.Provider == mananaSong.Brand && k.No == mananaSong.No);

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

        _context.Karaokes.Add(karaoke);
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
}
