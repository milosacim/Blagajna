using System.Windows.Controls;
using System.Windows.Media;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialLight.WPF;
using Syncfusion.Themes.Office2019Colorful.WPF;
using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.UI
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow(object dataContext)
        {
            DataContext = dataContext;

            MaterialLightThemeSettings materialLightThemeSettings = new MaterialLightThemeSettings();
            SfSkinManager.RegisterThemeSettings("MaterialLightBlue", materialLightThemeSettings);
            SfSkinManager.SetTheme(this, new Theme("MaterialLightBlue"));

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
