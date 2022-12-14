using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public interface IKomitentiViewModel
    {
        string? Header { get; set; }
        DockState State { get; set; }
        Task LoadAsync();
    }
}