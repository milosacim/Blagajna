﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MivexBlagajna.DataAccess;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Controls;
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
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace MivexBlagajna.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window? window = serviceProvider.GetService<MainWindow>();
            window.WindowState = WindowState.Maximized;
            window?.Show();
            base.OnStartup(e);
        }

        private string GetConnectionString(string name)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetConnectionString(name);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<MivexBlagajnaDbContext>(options => {

                options.UseSqlServer(GetConnectionString("TestDatabase"));
                options.UseTriggers(triggerOptions => {
                    triggerOptions.AddTrigger<SoftDeleteTrigger>();
                });

            }, ServiceLifetime.Singleton);
                

            services.AddTransient<IKomitentRepository, KomitentRepository>();
            services.AddTransient<IMestoTroskaRepository, MestoTroskaRepository>();
            services.AddTransient<IKontoRepository, KontoRepository>();
            services.AddTransient<ITransakcijeRepository, TransakcijeRepository>();

            services.AddTransient<ILookupKomitentDataService, LookupKomitentDataService>();
            services.AddTransient<ILookupMestoTroskaDataService, LookupMestoTroskaDataService>();

            services.AddSingleton<IKomitentiDetailViewModel, KomitentiDetailViewModel>();

            services.AddSingleton<IKomitentiNavigationViewModel, KomitentiNavigationViewModel>();

            services.AddTransient<Func<IKomitentiDetailViewModel>>(s => () => s.GetService<IKomitentiDetailViewModel>());
            services.AddTransient<Func<IMestaTroskaDetailsViewModel>>(s => () => s.GetService<IMestaTroskaDetailsViewModel>());

            services.AddSingleton<IMestaTroskaNavigationViewModel, MestaTroskaNavigationViewModel>();
            services.AddSingleton<IMestaTroskaDetailsViewModel, MestaTroskaDetailsViewModel>();
            services.AddSingleton<IUplateIsplateViewModel, UplateIsplateViewModel>();

            services.AddSingleton<MestaTroskaViewModel>();
            services.AddSingleton<KomitentiViewModel>();
            services.AddSingleton<UplateIsplateViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddSingleton<DockingAdapter>();
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
