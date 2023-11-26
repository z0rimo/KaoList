// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Services.Songs;

public class SongScoreService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SongScoreService(
        IServiceScopeFactory serviceScopeFactory
        )
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task UpdatePoplularDailySings()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        var today = DateTime.UtcNow.Date;

        context.PopularDailySings.RemoveRange(context.PopularDailySings.Where(p => p.Created < today));

        var searchScores = await context.SongSearchLogs
            .Where(log => log.Created >= today)
            .GroupBy(log => log.SingId)
            .Select(g => new SongRecord { SingId = g.Key, Score = g.Count() * 0.1 })
            .ToListAsync();

        var detailScores = await context.SongDetailLogs
            .Where(log => log.Created >= today)
            .GroupBy(log => log.SingId)
            .Select(g => new SongRecord { SingId = g.Key, Score = g.Count() * 0.5 })
            .ToListAsync();

        var followerScores = await context.SingFollowers
            .Where(log => log.Created >= today)
            .GroupBy(log => log.SingId)
            .Select(g => new SongRecord { SingId = g.Key, Score = g.Count() * 1 })
            .ToListAsync();

        var combinedScores = detailScores.Concat(searchScores).Concat(followerScores)
            .GroupBy(x => x.SingId)
            .Select(group => new { SingId = group.Key, TotalScore = group.Sum(x => x.Score) })
            .OrderByDescending(x => x.TotalScore)
            .ToList();

        foreach (var score in combinedScores)
        {
            context.PopularDailySings.Add(new PopularDailySing
            {
                SingId = score.SingId,
                Score = score.TotalScore,
                Created = today
            });
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdatePopularSings()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        var dailyScores = await context.PopularDailySings
            .GroupBy(p => p.SingId)
            .Select(group => new { SingId = group.Key, TotalScore = group.Sum(g => g.Score) })
            .ToListAsync();

        foreach (var score in dailyScores)
        {
            var popularSing = await context.PopularSings
                .FirstOrDefaultAsync(p => p.SingId == score.SingId);

            if (popularSing == null)
            {
                popularSing = new PopularSing
                {
                    SingId = score.SingId,
                    Score = score.TotalScore,
                    Created = DateTime.UtcNow
                };
                context.PopularSings.Add(popularSing);
            }
            else
            {
                popularSing.Score += score.TotalScore;
            }
        }

        await context.SaveChangesAsync();
    }
}
