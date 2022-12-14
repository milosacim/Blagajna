using MivexBlagajna.UI.Wrappers;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        Task LoadAsync();
        Task SaveTransakcijaAsync();
        Task DeleteTransakcijaAsync(TransakcijaWrapper transakcija);
        bool HasChanges { get; set; }
        TransakcijaWrapper Transakcija { get; }
    }
}