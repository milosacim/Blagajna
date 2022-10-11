namespace MivexBlagajna.UI.ViewModels.Komitenti.Navigation
{
    public class KomitentiNavigationItemViewModel : ObservableObject
    {
        private string _punNaziv;
        private bool _pravnoLice;
        private bool _fizickoLice;
        private string _mestoTroska;
        private bool _isSelected;

        public KomitentiNavigationItemViewModel(int id, string punNaziv, bool pravnoLice, bool fizickoLice, string mesto)
        {
            Id = id;
            _punNaziv = punNaziv;
            _pravnoLice = pravnoLice;
            _fizickoLice = fizickoLice;
            _mestoTroska = mesto;
            _isSelected = false;
        }

        public int Id { get; }
        public string PunNaziv
        {
            get { return _punNaziv; }
            set
            {
                _punNaziv = value;
                var oldValue = _punNaziv;
                OnObjectPropertyChanged(oldValue, value);
            }
        }
        public bool PravnoLice
        {
            get { return _pravnoLice; }
            set
            {
                _pravnoLice = value;
                var oldValue = _pravnoLice;
                OnObjectPropertyChanged(oldValue, value);
            }
        }
        public bool FizickoLice
        {
            get { return _fizickoLice; }
            set
            {
                _fizickoLice = value;
                var oldValue = _fizickoLice;
                OnObjectPropertyChanged(oldValue, value);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                var oldValue = _isSelected;
                OnObjectPropertyChanged(oldValue, value);
            }
        }


        public string? MestoTroska
        {
            get { return _mestoTroska; }
            set { 
                _mestoTroska = value;
                var oldValue = _mestoTroska;
                OnObjectPropertyChanged(oldValue, value); 
            }
        }

    }
}
