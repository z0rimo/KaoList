// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Services;

using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CodeRabbits.KaoList.Web.Services.Songs;

public class DailyTaskService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer? _timer;

    public DailyTaskService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var songService = scope.ServiceProvider.GetRequiredService<SongService>();
        var task1 = songService.FetchAndSaveSongsToDb();
        var songScoreService = scope.ServiceProvider.GetRequiredService<SongScoreService>();
        var task2 = songScoreService.UpdatePoplularDailySings();
        var task3 = songScoreService.UpdatePopularSings();

        await task2;
        await task3;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}
