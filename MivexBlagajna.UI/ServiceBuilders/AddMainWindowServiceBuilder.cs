using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.UI.ViewModels;
using System;

namespace MivexBlagajna.UI.ServiceBuilders
{
    public static class AddMainWindowServiceBuilder
    {
        public static IHostBuilder AddMainWindow(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
