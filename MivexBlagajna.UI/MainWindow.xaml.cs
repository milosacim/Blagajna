using System.Windows.Controls;
using System.Windows.Media;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.Office2019Colorful.WPF;
using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.UI
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow(object dataContext)
        {
            DataContext = dataContext;

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
