using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MivexBlagajna.UI.ServiceBuilders;
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
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddDbContext()
                .AddDataServices()
                .AddDialogService()
                .AddViewModels()
                .AddMainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            Window? window = _host.Services.GetRequiredService<MainWindow>();
            window.WindowState = WindowState.Maximized;
            window?.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Desila se neočekivana greška. Obavestite administratora." + Environment.NewLine + e.Exception.Message, "Unexpected Error");
            e.Handled = true;
        }
    }
}
