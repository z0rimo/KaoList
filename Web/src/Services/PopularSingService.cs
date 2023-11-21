// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net.Sockets;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CodeRabbits.KaoList.Web.Services;

public class PopularSingService
{
    private readonly KaoListDataContext _context;

    public PopularSingService(
        KaoListDataContext context
        )
    {
        _context = context;
    }

    /*private async Dictionary<string, double> CalculateScoresAsync()
    {
        var scores = new Dictionary<string, double>();

        var songDetailLogs = await _context.SongDetailLogs.ToListAsync();
        foreach (var log in songDetailLogs)
        {
            if (log.SingId is not null)
            {
                scores[log.SingId] = scores.GetValueOrDefault(log.SingId, 0) + 1;
            }
        }

        var songDetailScores = await _context.SongDetailLogs
            .GroupBy(log => log.SingId ?? "")
            .Select(group => new { SingId = group.Key, Count = group.Count() })
            .ToDictionaryAsync(x => x.SingId, x => x.Count);

        

        var songSearchLogs = await _context.SongSearchLogs.ToListAsync();
        foreach (var log in songSearchLogs)
        {
            if (log.)
        }

        return scores;
    }*/

    private async Task SaveScoreAsync(Dictionary<string, double> scores)
    {
        foreach (var log in scores)
        {
            var popularSing = new PopularSing
            {
                SingId = log.Key,
                Score = log.Value,
            };

            _context.PopularSings.Add(popularSing);
        }

        await _context.SaveChangesAsync();
    }
}
