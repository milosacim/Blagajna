using MivexBlagajna.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public interface IUplateIsplateViewModel
    {
        Task LoadAsync();
        Task SaveTransakcijaAsync();
        void CreateBrojNaloga(object? obj);
    }
}