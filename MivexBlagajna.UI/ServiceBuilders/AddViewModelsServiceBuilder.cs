using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.ViewModels;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Details;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.ViewModels.Komitenti.Navigation;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using MivexBlagajna.UI.Views.Services;
using System;

namespace MivexBlagajna.UI.ServiceBuilders
{
    public static class AddViewModelsServiceBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient(CreateKomitentiDetailViewModel);
                services.AddTransient(CreateKomitentiNavigationViewModel);
                services.AddTransient(CreateKomitentiViewModel);

                services.AddTransient(CreateMestaTroskaDetailsViewModel);
                services.AddTransient(CreateMestaTroskaNavigationViewModel);
                services.AddTransient(CreateMestaTroskaViewModel);

                services.AddTransient(CreateUplateIsplateViewModel);

                services.AddTransient<Func<KomitentiViewModel>>(services => () => services.GetRequiredService<KomitentiViewModel>());
                services.AddTransient<Func<MestaTroskaViewModel>>(services => () => services.GetRequiredService<MestaTroskaViewModel>());
                services.AddTransient<Func<UplateIsplateViewModel>>(services => () => services.GetRequiredService<UplateIsplateViewModel>());

                services.AddTransient<MainViewModel>();

            });

            return host;
        }

        private static KomitentiViewModel CreateKomitentiViewModel(IServiceProvider services)
        {
            return new KomitentiViewModel(
                services.GetRequiredService<IKomitentiNavigationViewModel>(),
                services.GetRequiredService<IKomitentiDetailViewModel>(),
                services.GetRequiredService<IMessageDialogService>()
            );
        }

        private static KomitentiNavigationViewModel CreateKomitentiNavigationViewModel(IServiceProvider services)
        {
            return new KomitentiNavigationViewModel(
                services.GetRequiredService<ILookupKomitentDataService>()
            );
        }

        private static KomitentiDetailViewModel CreateKomitentiDetailViewModel(IServiceProvider services)
        {
            return new KomitentiDetailViewModel(
                services.GetRequiredService<IKomitentRepository>(),
                services.GetRequiredService<IMessageDialogService>()
            );
        }

        private static MestaTroskaViewModel CreateMestaTroskaViewModel(IServiceProvider services)
        {
            return new MestaTroskaViewModel(
                services.GetRequiredService<IMestaTroskaNavigationViewModel>(),
                services.GetRequiredService<IMestaTroskaDetailsViewModel>()
            );
        }

        private static MestaTroskaDetailsViewModel CreateMestaTroskaDetailsViewModel(IServiceProvider services)
        {
            return new MestaTroskaDetailsViewModel(
                services.GetRequiredService<IMestoTroskaRepository>(),
                services.GetRequiredService<IMessageDialogService>()
            );
        }

        private static MestaTroskaNavigationViewModel CreateMestaTroskaNavigationViewModel(IServiceProvider services)
        {
            return new MestaTroskaNavigationViewModel(
                services.GetRequiredService<ILookupMestoTroskaDataService>()
            );
        }

        private static UplateIsplateViewModel CreateUplateIsplateViewModel(IServiceProvider services)
        {
            return new UplateIsplateViewModel(
                services.GetRequiredService<ITransakcijeRepository>(),
                services.GetRequiredService<IMessageDialogService>()
            );
        }

    }
}
