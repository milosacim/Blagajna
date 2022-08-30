using System;
using System.Windows;
using System.Windows.Controls;
using MivexBlagajna.UI.ViewModels;
using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.UI
{
    public partial class MainWindow : RibbonWindow
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
            DockingManager manager = (dockingadapter.Content as Grid).Children[0] as DockingManager;
            manager.CloseButtonClick += Manager_CloseButtonClick;
        }

        private void Manager_CloseButtonClick(object sender, CloseButtonEventArgs e)
        {
            var test = e.TargetItem;
            var type = test.GetType().GetProperty(nameof(Content)).GetValue(test);
            
        }

        private void UpdateWorkspaces(UIElement item)
        {
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
