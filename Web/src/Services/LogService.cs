// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;

namespace CodeRabbits.KaoList.Web.Services;

public class LogService
{
    private readonly KaoListDataContext _context;

    public LogService(KaoListDataContext context)
    {
        _context = context;
    }

    public async Task CreateSongSearchLogAsync(string query, string? singId, string? userId,
                                               string token, string? itemTitle, string? artistName = null)
    {
        var log = new SongSearchLog
        {
            Query = query,
            SingId = singId,
            UserId = userId,
            IdentityToken = token,
            ItemTitle = itemTitle,
            ArtistName = artistName
        };

        _context.SongSearchLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task CreateSongDetailLogAsync(string id, string? userId, string token)
    {
        var log = new SongDetailLog
        {
            SingId = id,
            UserId = userId,
            IdentityToken = token
        };

        _context.SongDetailLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAppLogAsync(string message)
    {
        var log = new AppLog
        {
            Log = message
        };

        _context.AppLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
