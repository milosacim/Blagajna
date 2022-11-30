using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.DataAccess.Services.Repositories;

namespace MivexBlagajna.UI.ServiceBuilders
{
    public static class AddDataServicesServiceBuilder
    {
        public static IHostBuilder AddDataServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IKomitentRepository, KomitentRepository>();
                services.AddSingleton<IMestoTroskaRepository, MestoTroskaRepository>();
                services.AddSingleton<IKontoRepository, KontoRepository>();
                services.AddSingleton<ITransakcijeRepository, TransakcijeRepository>();

                services.AddSingleton<ILookupKomitentDataService, LookupKomitentDataService>();
                services.AddSingleton<ILookupMestoTroskaDataService, LookupMestoTroskaDataService>();
            });

            return host;
        }
    }
}
