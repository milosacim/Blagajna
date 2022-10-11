using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public interface IKomitentiViewModel
    {
        string? Header { get; set; }
        DockState State { get; set; }
        AsyncCommand CreateNewKomitentCommand { get; }
        void Dispose();
        Task LoadAsync();
    }
}