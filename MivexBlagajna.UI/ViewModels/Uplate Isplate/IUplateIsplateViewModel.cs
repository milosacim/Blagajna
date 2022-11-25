using MivexBlagajna.UI.Wrappers;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        Task LoadAsync();
        Task SaveTransakcijaAsync();
        Task DeleteTransakcijaAsync(TransakcijaWrapper transakcija);
        void CancelChange(object? obj);
        void CreateBrojNaloga(object? obj);
        bool HasChanges { get; set; }
        TransakcijaWrapper Transakcija { get; }
    }
}