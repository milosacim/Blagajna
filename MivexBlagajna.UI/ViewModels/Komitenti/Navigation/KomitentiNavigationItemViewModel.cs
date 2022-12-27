using System;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Navigation
{
    public class KomitentiNavigationItemViewModel : ViewModelBase
    {
        private string _punNaziv;
        private bool _pravnoLice;
        private bool _fizickoLice;
        private bool _isSelected;
        private string _adresa;
        private string _postBroj;
        private string _mesto;
        private string _telefon;

        public KomitentiNavigationItemViewModel(int id, string punNaziv, bool pravnoLice, bool fizickoLice, string adresa, string postBroj, string mesto, string telefon)
        {
            Id = id;
            _punNaziv = punNaziv;
            _pravnoLice = pravnoLice;
            _fizickoLice = fizickoLice;
            _adresa = adresa;
            _postBroj = postBroj;
            _mesto = mesto;
            _telefon = telefon;
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
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public bool PravnoLice
        {
            get { return _pravnoLice; }
            set
            {
                _pravnoLice = value;
                var oldValue = _pravnoLice;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public bool FizickoLice
        {
            get { return _fizickoLice; }
            set
            {
                _fizickoLice = value;
                var oldValue = _fizickoLice;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public string Adresa
        {
            get { return _adresa; }
            set
            {
                _adresa = value;
                var oldValue = _adresa;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public string Mesto
        {
            get { return _mesto; }
            set
            {
                _mesto = value;
                var oldValue = _mesto;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public string PostanskiBroj
        {
            get { return _postBroj; }
            set
            {
                _postBroj = value;
                var oldValue = _postBroj;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public string Kontakt
        {
            get { return _telefon; }
            set
            {
                _telefon = value;
                var oldValue = _telefon;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                var oldValue = _isSelected;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
