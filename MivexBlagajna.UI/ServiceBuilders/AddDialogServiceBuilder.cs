using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.UI.Views.Services;

namespace MivexBlagajna.UI.ServiceBuilders
{
    public static class AddDialogServiceBuilder
    {
        public static IHostBuilder AddDialogService(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<IMessageDialogService, MessageDialogService>();
            });

            return host;
        }
    }
}
