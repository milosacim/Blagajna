using MivexBlagajna.DataAccess.Services.Lookups;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{

    public class MestaTroskaNavigationViewModel : ViewModelBase, IMestaTroskaNavigationViewModel
    {
        private readonly ILookupMestoTroskaDataService _lookupMestoTroskaDataService;
        private MestaTroskaNavigationItemViewModel? _selectedMestoTroska;

        public event EventHandler<MestoTroskaArgs> OnMestoSelected;

        public MestaTroskaNavigationViewModel(ILookupMestoTroskaDataService lookupMestoTroskaDataService)
        {
            _lookupMestoTroskaDataService = lookupMestoTroskaDataService;

            MestaTroska = new ObservableCollection<MestaTroskaNavigationItemViewModel>();
        }

        public ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }
        public MestaTroskaNavigationItemViewModel SelectedMestoTroska
        {
            get { return _selectedMestoTroska; }
            set
            {
                var oldValue = _selectedMestoTroska;
                _selectedMestoTroska = value;
                OnModelPropertyChanged(oldValue, value);

                if (oldValue != value)
                {
                    OnMestoSelected?.Invoke(this, new MestoTroskaArgs(_selectedMestoTroska.Id, _selectedMestoTroska.Nadredjeni_Id, _selectedMestoTroska.IsSelected, oldValue == null ? null : oldValue.Id));
                }
            }
        }
        public async override Task LoadAsync()
        {
            MestaTroska.Clear();

            var lookup = await _lookupMestoTroskaDataService.GetLookupMestoTroskaAsync();

            foreach (var item in lookup)
            {
                if (item != null)
                {
                    MestaTroska.Add(new MestaTroskaNavigationItemViewModel(item.Id, item.Sifra, item.Naziv, item.NadredjenoMesto_Id));
                }
            }
            SelectedMestoTroska = MestaTroska.FirstOrDefault();
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
