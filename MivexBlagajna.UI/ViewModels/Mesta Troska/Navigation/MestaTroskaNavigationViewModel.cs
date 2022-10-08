using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.Events.Mesta_Troska;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
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
        private MestaTroskaNavigationItemViewModel? _selectedMestoTroska;
        public MestaTroskaNavigationViewModel(ILookupMestoTroskaDataService lookupMestoTroskaDataService
            )
        {
            _lookupMestoTroskaDataService = lookupMestoTroskaDataService;
            MestaTroska = new ObservableCollection<MestaTroskaNavigationItemViewModel>();
        }

        public delegate Task OnOpenMestoTroskaDetails(object sender, MestoTroskaArgs e);
        public event OnOpenMestoTroskaDetails? OpenDetails;
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
                    OpenDetails?.Invoke(this, new MestoTroskaArgs(_selectedMestoTroska.Id));
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

    // Args for opening Mesto troska details
    public class MestoTroskaArgs
    {
        public readonly int id;

        public MestoTroskaArgs(int id)
        {
            this.id = id;
        }
    }
}
