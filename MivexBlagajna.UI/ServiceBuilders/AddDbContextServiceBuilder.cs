using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.DataAccess;
using System.IO;

namespace MivexBlagajna.UI.ServiceBuilders
{
    public static class AddDbContextServiceBuilder
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddDbContext<MivexBlagajnaDbContext>(options =>
                {

                    options.UseSqlServer(GetConnectionString("TestDatabase"));
                    options.UseTriggers(triggerOptions =>
                    {
                        triggerOptions.AddTrigger<SoftDeleteTrigger>();
                    });

                }, ServiceLifetime.Singleton);
            });

            return host;
        }
        private static string GetConnectionString(string name)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetConnectionString(name);
        }
    }
}

