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

        public MestaTroskaNavigationItemViewModel(int id, string sifra, string naziv)
        {
            Id = id;
            Sifra = sifra;
            Naziv = naziv;
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


    }
}
