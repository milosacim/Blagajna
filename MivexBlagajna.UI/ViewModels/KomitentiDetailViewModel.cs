using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        private readonly IKomitentDataService _komitentDataService;
        private readonly IEventAggregator _eventAggregator;
        private Komitent _komitent;


        public KomitentiDetailViewModel(IKomitentDataService komitentDataService
            , IEventAggregator eventAggregator)
        {
            _komitentDataService = komitentDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenKomitentDetailViewEvent>().Subscribe(OnOpenKomitentDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanEnecute);
            {

            };
        }
        7
        private async void OnSaveExecute()
        {
            await _komitentDataService.SaveAsync(Komitent);
            _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Publish(
                new AfterKomitentSavedEventArgs
                {
                    Id = Komitent.Id,
                    PunNaziv = Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}"
                });
        }

        private bool OnSaveCanEnecute()
        {
            //TODO: Proveriti da li je validan Komitent
            return true;
        }

        private async void OnOpenKomitentDetailView(int komitentId)
        {
            await LoadAsync(komitentId);
        }

        public async Task LoadAsync(int komitentId)
        {
            Komitent = await _komitentDataService.GetByIdAsync(komitentId);
        }

        public Komitent Komitent
        {
            get { return _komitent; }
            set { _komitent = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

    }
}
