// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Services
{
    public class SongPopularityService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SongPopularityService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        public async Task UpdateDailyPopularSingsAsync()
        {
            var context = CreateScopedDataContext();

            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
            DateTime utcNow = DateTime.UtcNow;
            DateTime kstNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, kstZone);
            DateTime startDate = new DateTime(kstNow.Year, kstNow.Month, kstNow.Day, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime endDate = startDate.AddHours(24);

            // Popular Sings today based on detail logs, ordered by score
            var popularSingsToday = await context.SongDetailLogs
                .Where(log => log.Created >= startDate && log.Created < endDate)
                .GroupBy(log => log.SingId)
                .Select(group => new { SingId = group.Key, Score = group.Count() })
                .OrderByDescending(x => x.Score)
                .ToListAsync();

            // Remove existing records for the day
            context.PopularDailySings.RemoveRange(context.PopularDailySings);
            await context.SaveChangesAsync();

            // Insert new records based on popular sings today
            foreach (var sing in popularSingsToday)
            {
                context.PopularDailySings.Add(new PopularDailySing
                {
                    SingId = sing.SingId,
                    Created = startDate,
                    Score = sing.Score
                });
            }

            // Fill the remaining slots if popularSingsToday has fewer than 100 records
            if (popularSingsToday.Count < 100)
            {
                var remaining = 100 - popularSingsToday.Count;
                var uniquePopularSingIds = popularSingsToday.Select(s => s.SingId).ToList();

                var latestSings = await context.Sings
                    .Where(s => !uniquePopularSingIds.Contains(s.Id))
                    .OrderByDescending(s => s.Created)
                    .Take(remaining)
                    .Select(s => s.Id)
                    .ToListAsync();

                foreach (var singId in latestSings)
                {
                    context.PopularDailySings.Add(new PopularDailySing
                    {
                        SingId = singId,
                        Created = startDate,
                        Score = 0
                    });
                }
            }

            // Save changes
            await context.SaveChangesAsync();
        }

        public async Task UpdateAllTimePopularSingsAsync()
        {
            var context = CreateScopedDataContext();
            var existingRecords = await context.PopularSings.ToDictionaryAsync(x => x.SingId!, x => x.Score);

            var allTimePopularSings = await context.SongDetailLogs
                .GroupBy(log => log.SingId)
                .Select(group => new { SingId = group.Key, Score = group.Count() })
                .ToListAsync();

            var updatedRecords = new Dictionary<string, int>();
            foreach (var item in allTimePopularSings)
            {
                var newScore = existingRecords.ContainsKey(item.SingId!) ? item.Score + existingRecords[item.SingId!] : item.Score;
                updatedRecords[item.SingId!] = (int)newScore!;
            }

            var sortedAllTimePopularSings = updatedRecords.OrderByDescending(x => x.Value).Take(100).ToList();

            context.PopularSings.RemoveRange(context.PopularSings);
            await context.SaveChangesAsync();

            var now = DateTime.UtcNow;

            foreach (var sing in sortedAllTimePopularSings)
            {
                context.PopularSings.Add(new PopularSing
                {
                    SingId = sing.Key,
                    Created = now,
                    Score = sing.Value
                });
            }

            if (sortedAllTimePopularSings.Count < 100)
            {
                var remaining = 100 - sortedAllTimePopularSings.Count;

                var uniqueAllTimePopularSingIds = sortedAllTimePopularSings.Select(s => s.Key).ToList();

                var latestSings = await context.Sings
                    .Where(s => !uniqueAllTimePopularSingIds.Contains(s.Id!))
                    .OrderByDescending(s => s.Created)
                    .Take(remaining)
                    .Select(s => s.Id)
                    .ToListAsync();

                foreach (var singId in latestSings)
                {
                    context.PopularSings.Add(new PopularSing
                    {
                        SingId = singId,
                        Created = now,
                        Score = 0
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
