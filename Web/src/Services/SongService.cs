// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Newtonsoft.Json;
using System.IO;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;

public class SongService
{
    private readonly KaoListDataContext _context;

    public SongService(KaoListDataContext context)
    {
        _context = context;
    }

    public void DeleteAll()
    {
        var allInstrumentals = _context.Instrumental.ToList(); // 1. 모든 데이터 가져오기
        _context.Instrumental.RemoveRange(allInstrumentals);    // 2. 가져온 데이터 삭제하기

        var allSings = _context.Sings.ToList();
        _context.Sings.RemoveRange(allSings);

        _context.SaveChanges();                                // 3. 변경 사항 저장하기
    }

    public void ConvertAndSaveJsonToDb()
    {
        var jsonFilePath = "output-allbrands.json";
        var jsonContent = File.ReadAllText(jsonFilePath);
        var songs = JsonConvert.DeserializeObject<List<Song>>(jsonContent);

        foreach (var song in songs)
        {
            DateTime? releaseDate = DateTime.MinValue;
            if (song.Release != "0000-00-00")
            {
                releaseDate = DateTime.Parse(song.Release);
            }

            var instrumental = new Instrumental
            {
                Title = song.Title,
                NormalizedTitle = song.Title.ToUpper(),
                SoundId = null,
                Composer = song.Composer,
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

            // 노래의 singer 속성에 해당하는 사용자를 Users 테이블에서 찾기
            var user = _context.Users.FirstOrDefault(u => u.NickName == song.Singer);

            // 해당 사용자가 존재하면 SingUsers 테이블에 항목 추가
            if (user != null)
            {
                var singUser = new SingUser
                {
                    SingId = sing.Id,
                    UserId = user.Id
                };

                // 중복 추가를 피하기 위한 검사
                if (!_context.SingUsers.Any(su => su.SingId == singUser.SingId && su.UserId == singUser.UserId))
                {
                    _context.SingUsers.Add(singUser);
                }
            }

            var existingKaraoke = _context.Karaokes.FirstOrDefault(k => k.Provider == song.Brand && k.No == song.No);
            if (existingKaraoke == null)
            {
                var karaoke = new Karaoke
                {
                    Provider = song.Brand,
                    Created = releaseDate,
                    No = song.No,
                    SingId = sing.Id
                };
                _context.Karaokes.Add(karaoke);
            }
            else
            {
                continue;
            }
        }

        _context.SaveChanges();  // 모든 객체를 저장
    }

}

public class Song
{
    public string No { get; set; }
    public string Brand { get; set; }
    public string Title { get; set; }
    public string Singer { get; set; }
    public string Composer { get; set; }
    public string Lyricist { get; set; }
    public string Release { get; set; }
}
