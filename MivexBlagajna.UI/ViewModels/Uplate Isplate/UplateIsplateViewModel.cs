using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public class UplateIsplateViewModel : ViewModelBase, IDockElement
    {

        private readonly DockState _dockState;
        private readonly string _header;
        private readonly IKomitentRepository _komitentRepository;

        private string? _searchKomitentText;


        public UplateIsplateViewModel(IKomitentRepository komitentRepository
            )
        {
            _dockState = DockState.Document;
            _header = "Uplate / Isplate";
            _komitentRepository = komitentRepository;

            Komitenti = new ObservableCollection<KomitentWrapper>();
        }

        public DockState State
        {
            get { return _dockState; }
            set { }
        }

        public string Header
        {
            get { return _header; }
            set { }
        }


        public string SearchKomitentText
        {
            get { return _searchKomitentText; }
            set { _searchKomitentText = value; OnModelPropertyChanged(); OnModelPropertyChanged(nameof(FilteredKomitent)); }
        }

        public KomitentWrapper? FilteredKomitent
        {
            get { return SearchKomitentText != null ? Komitenti.FirstOrDefault(x => x.Sifra.ToString().Equals(SearchKomitentText, StringComparison.OrdinalIgnoreCase)) : null; }
        }

        public ObservableCollection<KomitentWrapper> Komitenti { get; }

        public override async Task LoadAsync()
        {
            var listOfKomitenti = await _komitentRepository.GetAll();
            Komitenti.Clear();
            foreach (var item in listOfKomitenti)
            {
                var komitent = new KomitentWrapper(item);
                Komitenti.Add(komitent);
            }
        }
    }
}
