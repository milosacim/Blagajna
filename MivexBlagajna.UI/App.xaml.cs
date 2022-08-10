﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MivexBlagajna.DataAccess;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
                options.UseSqlServer("Server=192.168.0.144;Database=MivexBlagajnaDb;User Id=retail01;Password=mivex***032;"), ServiceLifetime.Scoped );

            services.AddScoped<MainWindow>();

            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IKomitentDataService, KomitentiDataService>();
            services.AddSingleton<ILookupKomitentDataService, LookupKomitentDataService>();
            services.AddSingleton<IKomitentiNavigationViewModel, KomitentiNavigationViewModel>();
            services.AddSingleton<IKomitentiDetailViewModel, KomitentiDetailViewModel>();

            services.AddScoped<Func<MivexBlagajnaDbContext>>(s => () => s.GetService<MivexBlagajnaDbContext>());
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<KomitentiViewModel>();
            services.AddSingleton<MestaTroskaViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
