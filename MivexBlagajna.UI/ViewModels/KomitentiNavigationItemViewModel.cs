using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiNavigationItemViewModel : ViewModelBase
    {
        private string _punNaziv;

        public KomitentiNavigationItemViewModel(int id, string punNaziv)
        {
            Id = id;
            _punNaziv = punNaziv;
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
    }
}
