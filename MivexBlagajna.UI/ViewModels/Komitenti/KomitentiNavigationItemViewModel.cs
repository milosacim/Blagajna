using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public class KomitentiNavigationItemViewModel : ViewModelBase
    {
        private string _punNaziv;
        private bool _pravnoLice;
        private bool _fizickoLice;

        public KomitentiNavigationItemViewModel(int id, string punNaziv, bool fizickoLice = false, bool pravnoLice = false)
        {
            Id = id;
            _punNaziv = punNaziv;
            _pravnoLice = pravnoLice;
            _fizickoLice = fizickoLice;
        }

        public int Id { get; }

        public string PunNaziv
        {
            get { return _punNaziv; }
            set
            {
                _punNaziv = value;
                OnModelPropertyChanged();
            }
        }

        public bool PravnoLice
        {
            get { return _pravnoLice; }
            set { _pravnoLice = value; OnModelPropertyChanged(); }
        }

        public bool FizickoLice
        {
            get { return _fizickoLice; }
            set { _fizickoLice = value; OnModelPropertyChanged(); }
        }
    }
}
