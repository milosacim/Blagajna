using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Transakcije;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private MestoTroska _mesto;


        public FinansijskaKarticaViewModel( ITransakcijeRepository transakcijeRepository)
        {
            Header = "Finansijska Kartica";
            State = DockState.Document;

            Transakcije = new ObservableCollection<StavkaKartice>();
            Komitenti = new ObservableCollection<Komitent>();
            MestaTroska = new ObservableCollection<MestoTroska>();
            VrsteNaloga = new ObservableCollection<VrsteNaloga>();
            Konta = new ObservableCollection<Konto>();

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

        public Komitent Komitent
        {
            get { return _komitent; }
            set { _komitent = value; OnModelPropertyChanged(); }
        }

        public MestoTroska Mesto
        {
            get { return _mesto; }
            set { _mesto = value; OnModelPropertyChanged(); }
        }

        public Konto Konto
        {
            get { return _konto; }
            set { _konto = value; OnModelPropertyChanged(); }
        }

        public VrsteNaloga Vrsta
        {
            get { return _vrstaNaloga; }
            set { _vrstaNaloga = value; OnModelPropertyChanged(); }
        }

        public ObservableCollection<Komitent> Komitenti { get; }
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public ObservableCollection<Konto> Konta { get; }
        public ObservableCollection<VrsteNaloga> VrsteNaloga { get; set; }
        public ObservableCollection<StavkaKartice> Transakcije { get; }

        public AsyncCommand LoadKarticaCommand { get; }

        public async override Task LoadAsync()
        {
            var komitenti = await _transakcijeRepository.GetAllKomitenti();
            var mesta = await _transakcijeRepository.GetAllMestaTroska();
            var konta = await _transakcijeRepository.GetAllKonta();
            var vrste = await _transakcijeRepository.GetAllVrsteNaloga();

            if (!Komitenti.Any())
            {
                Komitenti.Clear();
                foreach (var item in komitenti)
                {
                    Komitenti.Add(item);
                }
            }

            if (!MestaTroska.Any())
            {
                MestaTroska.Clear();
                foreach (var item in mesta)
                {
                    MestaTroska.Add(item);
                }
            }

            if (!Konta.Any())
            {
                Konta.Clear();
                foreach (var item in konta)
                {
                    Konta.Add(item);
                }
            }

            if (!VrsteNaloga.Any())
            {
                VrsteNaloga.Clear();
                foreach (var item in vrste)
                {
                    VrsteNaloga.Add(item);
                }
            }
        }

        public async Task LoadDataAsync()
        {
            var kontoUslov = Konto != null ? $" AND k.id = {Konto.Id}" : null;
            var komitentUslov = Komitent != null ? $" AND kom.Id = {Komitent.Id}" : null;
            var mestoUslov = Mesto != null ? $" AND mt.Id = {Mesto.Id}" : null;
            var vrstaUslov = Vrsta != null ? $" AND vn.Id = {Vrsta.Id}" : null;

            var uslov = $"\'(t.Datum >= \'\'{DatumOd.Date:yyyy-MM-dd HH:mm:ss}\'\' AND t.Datum <= \'\'{DatumDo.Date.AddDays(1):yyyy-MM-dd HH:mm:ss}\'\'){kontoUslov}{komitentUslov}{mestoUslov}{vrstaUslov}\'";

            var stavke = await _transakcijeRepository.GetFinansijskaKarticaAsync(uslov);

            if (Transakcije.Any())
            {
                Transakcije.Clear();
                GenerateStavke(stavke);
            }
            else
            {
                GenerateStavke(stavke);
            }
        }

        private void GenerateStavke(IEnumerable<StavkaKartice> stavke)
        {
            foreach (var item in stavke)
            {
                Transakcije.Add(item);
            }
        }
    }
}
