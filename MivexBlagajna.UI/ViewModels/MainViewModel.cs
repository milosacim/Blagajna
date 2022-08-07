using MivexBlagajna.Data.Models.UI_Models;
using System.Collections.ObjectModel;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Workspace> _workspaces;

        public MainViewModel(ObservableCollection<Workspace> workspaces)
        {
            _workspaces = workspaces;
        }

        public ObservableCollection<Workspace> Workspaces
        {
            get { return _workspaces; }
            set { _workspaces = value; }
        }
    }
}
