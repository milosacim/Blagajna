using MivexBlagajna.Data.Models;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        object SelectedKonto { get; set; }

        Task<Transakcija> CreateNewTransakcija();
        Task LoadAsync();
        object SelectKonto(VrsteNalogaEnum vrstaNaloga);
    }
}