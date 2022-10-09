namespace MivexBlagajna.UI.ViewModels.Komitenti.Navigation
{
    public class KomitentiNavigationItemViewModel : ViewModelBase
    {
        private string _punNaziv;
        private bool _pravnoLice;
        private bool _fizickoLice;
        private string _mestoTroska;

        public KomitentiNavigationItemViewModel(int id, string punNaziv, bool pravnoLice, bool fizickoLice, string mesto)
        {
            Id = id;
            _punNaziv = punNaziv;
            _pravnoLice = pravnoLice;
            _fizickoLice = fizickoLice;
            _mestoTroska = mesto;
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
            set
            {
                _pravnoLice = value;
                OnModelPropertyChanged();
            }
        }
        public bool FizickoLice
        {
            get { return _fizickoLice; }
            set
            {
                _fizickoLice = value;
                OnModelPropertyChanged();
            }
        }

        public string? MestoTroska
        {
            get { return _mestoTroska; }
            set { _mestoTroska = value; OnModelPropertyChanged(); }
        }

    }
}
