// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Data;
using System.Globalization;
using CodeRabbits.KaoList.Web.Services.Mananas;
using CodeRabbits.KaoList.Identity;
using Microsoft.EntityFrameworkCore;
using CodeRabbits.KaoList.Web.Services.YouTubes;
using CodeRabbits.KaoList.Web.Models.Searchs;

namespace CodeRabbits.KaoList.Web.Services;

public class SongService
{
    private readonly KaoListDataContext _context;
    private readonly MananaService _mananaService;
    private readonly YouTubeService _youTubeService;
    private readonly ILogger<SongService> _logger;

    public SongService(
        KaoListDataContext context,
        MananaService mananaService,
        YouTubeService youTubeService,
        ILogger<SongService> logger)
    {
        _context = context;
        _mananaService = mananaService;
        _youTubeService = youTubeService;
        _logger = logger;
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

    public async Task UpdateSoundIdUsingSearchSnippet(IEnumerable<SearchResource> searchResources)
    {
        foreach (var resource in searchResources)
        {
            var soundId = await CheckSoundIdAsync(resource.Id.Id);
            if (soundId is null)
            {
                var title = resource.Snippet?.Title;
                var nickname = resource.Snippet?.SongUsers?.FirstOrDefault()?.Nickname;
                var instId = await _context.Sings
                    .Where(s => s.Id == resource.Id.Id)
                    .Select(s => s.InstrumentalId)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(instId))
                {
                    await UpdateSoundIdAsync(instId, title, nickname);
                }
            }
        }
    }


    public async Task UpdateSoundIdAsync(string instId, string title, string nickname)
    {
        var videoId = await _youTubeService.SearchSoundIdAsync(title, nickname);

        if (!string.IsNullOrEmpty(videoId))
        {
            try
            {
                var newSound = new Sound { Path = videoId };
                _context.Sounds.Add(newSound);
                await _context.SaveChangesAsync();

                var instrumental = await _context.Instrumental.FirstOrDefaultAsync(i => i.Id == instId);

                if (instrumental != null)
                {
                    instrumental.SoundId = newSound.Id;
                    _context.Update(instrumental);
                    await _context.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while updating SoundId for Instrumental ID: {instId} - {ex.Message}");
            }
        }
    }

}
