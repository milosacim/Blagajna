using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        Task LoadAsync();
        Task SaveTransakcijaAsync();
        Task CancelChange();
        void CreateBrojNaloga(object? obj);
        bool HasChanges { get; set; }
    }
}