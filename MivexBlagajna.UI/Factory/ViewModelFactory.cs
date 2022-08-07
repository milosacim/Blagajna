using MivexBlagajna.UI.ViewModels;
using MivexBlagajna.UI.State;

namespace MivexBlagajna.UI.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<KomitentiViewModel> _createKomitentiViewModel;
        private readonly CreateViewModel<MestaTroskaViewModel> _createMestaTroskaViewModel;

        public ViewModelFactory(CreateViewModel<KomitentiViewModel> createKomitentiViewModel, CreateViewModel<MestaTroskaViewModel> createMestaTroskaViewModel)
        {
            _createKomitentiViewModel = createKomitentiViewModel;
            _createMestaTroskaViewModel = createMestaTroskaViewModel;
        }


        public ViewModelBase CreateViewModel(ViewModelType viewType)
        {
            switch (viewType)
            {
                case ViewModelType.Komitenti:
                    return _createKomitentiViewModel();
                    break;
                case ViewModelType.MostaTroska:
                    return _createMestaTroskaViewModel();
                    break;
                case ViewModelType.UplateIsplate:
                    return null;
                    break;
                case ViewModelType.FinansijskaKartica:
                    return null;
                    break;
                default: return null;
            }
        }
    }
}
