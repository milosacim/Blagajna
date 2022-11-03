using MivexBlagajna.Data.Models;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        Konto? SelectedKonto { get; set; }
        Task LoadAsync();
        Konto SelectKonto(VrsteKontaEnum vrstaNaloga);
    }
}