using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System;
using System.Threading.Tasks;

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

    }
}
