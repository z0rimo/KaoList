using CodeRabbits.KaoList.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeRabbits.KaoList.Web.Datas;

public class KaoListContext : KaoListDataContext
{
    public KaoListContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var bulider = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddEnvironmentVariables()
                                                    .AddUserSecrets(Assembly.GetAssembly(typeof(KaoListContext)));

            IConfigurationRoot configuration = bulider.Build();

            optionsBuilder.UseSqlServer(configuration["ASPNETCORE_CONNECTIONSTRING"],
                options => options.EnableRetryOnFailure()
            );
        }
    }
}
