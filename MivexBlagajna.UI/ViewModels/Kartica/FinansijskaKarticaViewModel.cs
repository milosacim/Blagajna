using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Transakcije;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Kartica
{
    public class FinansijskaKarticaViewModel : ViewModelBase, IDockElement
    {
        private readonly ITransakcijeRepository _transakcijeRepository;
        private DateTime _datumOd;
        private DateTime _datumDo;
        private VrsteNaloga _vrstaNaloga;
        private Konto _konto;
        private Komitent _komitent;

        public FinansijskaKarticaViewModel( ITransakcijeRepository transakcijeRepository)
        {
            Header = "Finansijska Kartica";
            State = DockState.Document;

            Transakcije = new ObservableCollection<Transakcija>();

            _datumDo= DateTime.UtcNow;
            _datumOd = DateTime.UtcNow;
            _transakcijeRepository = transakcijeRepository;

            LoadKarticaCommand = new LoadKarticaCommand(this);
        }
        public string? Header { get; private set; }
        public DockState State { get; private set; }

        public DateTime DatumOd
        {
            get { return _datumOd; }
            set { _datumOd = value; OnModelPropertyChanged(); }
        }

        public DateTime DatumDo
        {
            get { return _datumDo; }
            set { _datumDo = value; OnModelPropertyChanged(); }
        }

        public ObservableCollection<Transakcija> Transakcije { get; }

        public AsyncCommand LoadKarticaCommand { get;}

        public async Task LoadData(DateTime datum)
        {
            datum = DatumDo;
            var transakcije = await _transakcijeRepository.GetFinansijskaKarticaAsync(datum);
        }


    }
}
