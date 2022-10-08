using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public interface IMainViewModel
    {
        ViewModelBase? ActiveViewModel { get; set; }
        ViewModelBase? SelectedViewModel { get; set; }
        ObservableCollection<ViewModelBase> Workspaces { get; set; }
        Task SelectViewModel(object parameter);
    }
}