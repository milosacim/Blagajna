using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System;

namespace MivexBlagajna.UI.Navigation
{
    public class NavigationViewModel
    {
        private readonly Func<KomitentiViewModel> _komitentiViewModelCreator;
        private readonly Func<MestaTroskaViewModel> _mestaTroskaViewModelCreator;
        private readonly Func<UplateIsplateViewModel> _uplateIsplateViewModelCreator;

        public NavigationViewModel(
            Func<KomitentiViewModel> komitentiViewModelCreator
            , Func<MestaTroskaViewModel> mestaTroskaViewModelCreator
            , Func<UplateIsplateViewModel> uplateIsplateViewModelCreator
            )
        {
            _komitentiViewModelCreator = komitentiViewModelCreator;
            _mestaTroskaViewModelCreator = mestaTroskaViewModelCreator;
            _uplateIsplateViewModelCreator = uplateIsplateViewModelCreator;
        }
    }
}
