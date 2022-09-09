using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public class MestaTroskaNavigationViewModel : ViewModelBase, IMestaTroskaNavigationViewModel
    {
        private readonly ILookupMestoTroskaDataService _lookupMestoTroskaDataService;
        private readonly IEventAggregator _eventAggregator;
        private MestaTroskaNavigationItemViewModel _selectedMestoTroska;

        public MestaTroskaNavigationViewModel(ILookupMestoTroskaDataService lookupMestoTroskaDataService, IEventAggregator eventAggregator)
        {
            _lookupMestoTroskaDataService = lookupMestoTroskaDataService;
            _eventAggregator = eventAggregator;
            MestaTroska = new ObservableCollection<MestaTroskaNavigationItemViewModel>();
        }

        public ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }

        public MestaTroskaNavigationItemViewModel SelectedMestoTroska
        {
            get { return _selectedMestoTroska; }
            set
            {
                _selectedMestoTroska = value;
                OnModelPropertyChanged();

                if (_selectedMestoTroska != null)
                {
                    _eventAggregator.GetEvent<OnOpenMestoTroskaDetailsEvent>().Publish(_selectedMestoTroska.Id);
                }
            }
        }

        public async override Task LoadAsync()
        {
            var lookup = await _lookupMestoTroskaDataService.GetLookupMestoTroskaAsync();
            MestaTroska.Clear();
            foreach (var item in lookup)
            {
                if (item != null)
                {
                    MestaTroska.Add(new MestaTroskaNavigationItemViewModel(item.Id, item.Sifra, item.Naziv));
                }
            }
            SelectedMestoTroska = MestaTroska.First();
        }
    }
}
