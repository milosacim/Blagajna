using MivexBlagajna.Data.Models;

namespace MivexBlagajna.UI.ViewModels
{
    public class SingleKomitentViewModel : ViewModelBase
    {
        private Komitent _model;

        public SingleKomitentViewModel(Komitent model)
        {
            _model = model;
        }
        public int Id
        {
            get => _model.Id;
            set
            {
                _model.Id = value;
                OnPropertyChanged();
            }
        }
        public int Sifra
        {
            get => _model.Sifra;
            set
            {
                _model.Sifra = value;
                OnPropertyChanged();
            }
        }
        public string? Naziv
        {
            get => _model.Naziv;
            set
            {
                _model.Naziv = value;
                OnPropertyChanged();
            }
        }
        public string? Naziv2
        {
            get => _model.Naziv2;
            set
            {
                _model.Naziv2 = value;
                OnPropertyChanged();
            }
        }

        public string? Ime
        {
            get => _model.Ime;
            set
            {
                _model.Ime = value;
                OnPropertyChanged();
            }
        }
        public string? Prezime
        {
            get => _model.Prezime;
            set
            {
                _model.Prezime = value;
                OnPropertyChanged();
            }
        }
        public string? PostanskiBroj
        {
            get => _model.PostanskiBroj;
            set
            {
                _model.PostanskiBroj = value;
                OnPropertyChanged();
            }
        }
        public string? Mesto
        {
            get => _model.Mesto;
            set
            {
                _model.Mesto = value;
                OnPropertyChanged();
            }
        }
        public string? Adresa
        {
            get => _model.Adresa;
            set
            {
                _model.Adresa = value;
                OnPropertyChanged();
            }
        }
        public string? KontaktOsoba
        {
            get => _model.KontaktOsoba;
            set
            {
                _model.KontaktOsoba = value;
                OnPropertyChanged();
            }
        }
        public string? Telefon
        {
            get => _model.KontaktOsoba;
            set
            {
                _model.KontaktOsoba = value;
                OnPropertyChanged();
            }
        }
        public bool PravnoLice
        {
            get => _model.PravnoLice;
            set
            {
                _model.PravnoLice = value;
                OnPropertyChanged();
            }
        }
        public bool FizickoLice
        {
            get => _model.FizickoLice;
            set
            {
                _model.FizickoLice = value;
                OnPropertyChanged();
            }
        }
    }
}
