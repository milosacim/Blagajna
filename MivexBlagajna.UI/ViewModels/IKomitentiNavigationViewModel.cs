using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public interface IKomitentiNavigationViewModel
    {
        Task LoadAsync(string filter, bool pravno, bool fizicko);
    }
}