using CodeRabbits.KaoList.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeRabbits.KaoList.Web.Test;

public static class IWebHostBuilderExtension
{
    public static void ConfigureTestServices(this IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.Single(
                d => d.ServiceType == typeof(DbContextOptions<KaoListDataContext>));

            services.Remove(descriptor);

            services.AddDbContext<KaoListDataContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForPostTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<KaoListDataContext>();

            db.Database.EnsureCreated();

            Utilities.ReinitializeDbForTests(db);
        });
    }
}
