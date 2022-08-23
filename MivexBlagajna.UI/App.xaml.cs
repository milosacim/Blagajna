using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MivexBlagajna.DataAccess;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.ViewModels;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.Views.Services;
using Prism.Events;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MivexBlagajna.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window? window = serviceProvider.GetService<MainWindow>();
            window.WindowState = WindowState.Maximized;
            window?.Show();
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<MivexBlagajnaDbContext>( options => 
                options.UseSqlServer("Server=192.168.0.144;Database=MivexBlagajnaDb;User Id=retail01;Password=mivex***032;"), ServiceLifetime.Singleton );



            services.AddTransient<IKomitentRepository, KomitentRepository>();
            services.AddTransient<ILookupKomitentDataService, LookupKomitentDataService>();
            services.AddTransient<IKomitentiDetailViewModel, KomitentiDetailViewModel>();
            services.AddTransient<MestaTroskaViewModel>();
            services.AddTransient<IKomitentiNavigationViewModel, KomitentiNavigationViewModel>();
            services.AddTransient<Func<IKomitentiDetailViewModel>>(s => () => s.GetService<IKomitentiDetailViewModel>());
            services.AddTransient<IMessageDialogService, MessageDialogService>();

            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<KomitentiViewModel>();
            services.AddSingleton<MestaTroskaViewModel>();
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Desila se neočekivana greška. Obavestite administratora." + Environment.NewLine + e.Exception.Message, "Unexpected Error");
            e.Handled = true;
        }
    }
}
