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
            set
            {
                var oldValue = _sifra;
                _sifra = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public string Naziv
        {
            get { return _naziv; }
            set
            {
                var oldValue = _naziv;
                _naziv = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public int Nivo
        {
            get { return _nivo; }
            set
            {
                var oldValue = _nivo;
                _nivo = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public int Nadredjeni_Id
        {
            get { return _nadredjeniId; }
            set { 
                var oldValue = _nadredjeniId;
                _nadredjeniId = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
