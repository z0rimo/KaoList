// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;

namespace CodeRabbits.KaoList.Web.Services;

public class LogService
{
    private readonly KaoListDataContext _context;

    public LogService(
        KaoListDataContext context
        )
    {
        _context = context;
    }

    public async Task CreateSearchLogAsync(string query, string? userId, string token)
    {
        var log = new SongSearchLog
        {
            Query = query,
            UserId = userId,
            IdentityToken = token
        };

        _context.SongSearchLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
