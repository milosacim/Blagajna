using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.Events.Mesta_Troska;
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

            _eventAggregator.GetEvent<AfterMestoTroskaSavedEvent>().Subscribe(OnMestoTroskaSaved);
            _eventAggregator.GetEvent<OnMestoTroskaDeletedEvent>().Subscribe(OnMestoTroskaDeleted);
        }
        private void OnMestoTroskaSaved(AfterMestoTroskaSavedArgs obj)
        {
            var lookupitem = MestaTroska.SingleOrDefault(l => l.Id == obj.Id);

            if (lookupitem == null)
            {
                MestaTroska.Add(new MestaTroskaNavigationItemViewModel(obj.Id, obj.Sifra, obj.Naziv, obj.Nivo, obj.NadredjenoMesto_Id));
                SelectedMestoTroska = MestaTroska.First();
            }

            else { lookupitem.Sifra = obj.Sifra; lookupitem.Naziv = obj.Naziv; }
        }
        private void OnMestoTroskaDeleted(int id)
        {
            var mesto = MestaTroska.SingleOrDefault(m => m.Id == id);

            if (mesto != null)
            {
                MestaTroska.Remove(mesto);
                SelectedMestoTroska = MestaTroska.Last();
            }
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
                    _eventAggregator.GetEvent<OnOpenMestoTroskaDetailsEvent>().Publish(new OnOpenMestoTroskaDetailsArgs { Id = _selectedMestoTroska.Id, NadredjenoMesto_Id = _selectedMestoTroska.Nadredjeni_Id });
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
                    MestaTroska.Add(new MestaTroskaNavigationItemViewModel(item.Id, item.Sifra, item.Naziv, item.Nivo, item.NadredjenoMesto_Id));
                }
            }
            SelectedMestoTroska = MestaTroska.First();
        }
        public override void Dispose()
        {
            SelectedMestoTroska.Dispose();
            base.Dispose();
        }
    }
}
