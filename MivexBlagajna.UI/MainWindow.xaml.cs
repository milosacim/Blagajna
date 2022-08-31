using System;
using System.Linq;
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
            UpdateActiveTab(e);
        }

        private void UpdateActiveTab(CloseButtonEventArgs e)
        {
            ViewModelBase item = (ViewModelBase)e.TargetItem.GetType().GetProperty(nameof(Content)).GetValue(e.TargetItem);

            if (_viewModel.Workspaces.Contains(item))
            {
                _viewModel.Workspaces.Remove(item);

                if (_viewModel.Workspaces.Count == 0)
                {
                    _viewModel.ActiveViewModel = null;
                }
                else
                {
                    _viewModel.ActiveViewModel = _viewModel.Workspaces.Last();
                }
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
