using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MivexBlagajna.UI.ViewModels;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.FluentDark.WPF;
using Syncfusion.Themes.FluentLight.WPF;
using Syncfusion.Themes.MaterialLight.WPF;
using Syncfusion.Themes.Office2019Colorful.WPF;
using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.UI
{
    public partial class MainWindow : RibbonWindow
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            //MaterialLightThemeSettings materialLightThemeSettings = new MaterialLightThemeSettings();
            Office2019ColorfulThemeSettings office2019ColorfulThemeSettings = new Office2019ColorfulThemeSettings();
            office2019ColorfulThemeSettings.FontFamily = new FontFamily("Segoe UI");
            //FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
            //FluentDarkThemeSettings fluentDarkThemeSettings = new FluentDarkThemeSettings();

            //SfSkinManager.RegisterThemeSettings("MaterialLightBlue", materialLightThemeSettings);
            SfSkinManager.RegisterThemeSettings("Office2019Colorful", office2019ColorfulThemeSettings);
            //SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
            //SfSkinManager.RegisterThemeSettings("FluentDark", fluentDarkThemeSettings);

            //SfSkinManager.SetTheme(this, new Theme("FluentDark"));
            //SfSkinManager.SetTheme(this, new Theme("MaterialLightBlue"));
            SfSkinManager.SetTheme(this, new Theme("Office2019Colorful"));
            //SfSkinManager.SetTheme(this, new Theme("FluentLight"));

            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            DockingManager manager = (dockingadapter.Content as Grid).Children[0] as DockingManager;
            manager.CloseButtonClick += Manager_CloseButtonClick;
        }

        private void Manager_CloseButtonClick(object sender, CloseButtonEventArgs e)
        {
            IClosing context = DataContext as IClosing;

            if (context != null)
            {
                e.Cancel = !context.OnClosing();
            }
        }

    }
}
