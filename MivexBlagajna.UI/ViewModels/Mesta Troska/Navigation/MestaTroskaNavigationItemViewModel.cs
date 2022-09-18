using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public class MestaTroskaNavigationItemViewModel : ViewModelBase
    {
        private string _naziv;
        private string _sifra;
        private int _nivo;
        private int _nadredjeniId;

        public MestaTroskaNavigationItemViewModel(int id, string sifra, string naziv, int nivo, int nadredjeni)
        {
            Id = id;
            Sifra = sifra;
            Naziv = naziv;
            Nivo = nivo;
            Nadredjeni_Id = nadredjeni;

        }

        public int Id { get; }

        public string Sifra
        {
            get { return _sifra; }
            set { _sifra = value; OnModelPropertyChanged(); }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; OnModelPropertyChanged(); }
        }

        public int Nivo
        {
            get { return _nivo; }
            set { _nivo = value; OnModelPropertyChanged(); }
        }

        public int Nadredjeni_Id
        {
            get { return _nadredjeniId; }
            set { _nadredjeniId = value; OnModelPropertyChanged(); }
        }
    }
}
