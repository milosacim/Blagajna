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
                OnModelPropertyChanged();
            }
        }
        public int Sifra
        {
            get => _model.Sifra;
            set
            {
                _model.Sifra = value;
                OnModelPropertyChanged();
            }
        }
        public string? Naziv
        {
            get => _model.Naziv;
            set
            {
                _model.Naziv = value;
                OnModelPropertyChanged();
            }
        }
        public string? Naziv2
        {
            get => _model.Naziv2;
            set
            {
                _model.Naziv2 = value;
                OnModelPropertyChanged();
            }
        }

        public string? Ime
        {
            get => _model.Ime;
            set
            {
                _model.Ime = value;
                OnModelPropertyChanged();
            }
        }
        public string? Prezime
        {
            get => _model.Prezime;
            set
            {
                _model.Prezime = value;
                OnModelPropertyChanged();
            }
        }
        public string? PostanskiBroj
        {
            get => _model.PostanskiBroj;
            set
            {
                _model.PostanskiBroj = value;
                OnModelPropertyChanged();
            }
        }
        public string? Mesto
        {
            get => _model.Mesto;
            set
            {
                _model.Mesto = value;
                OnModelPropertyChanged();
            }
        }
        public string? Adresa
        {
            get => _model.Adresa;
            set
            {
                _model.Adresa = value;
                OnModelPropertyChanged();
            }
        }
        public string? KontaktOsoba
        {
            get => _model.KontaktOsoba;
            set
            {
                _model.KontaktOsoba = value;
                OnModelPropertyChanged();
            }
        }
        public string? Telefon
        {
            get => _model.KontaktOsoba;
            set
            {
                _model.KontaktOsoba = value;
                OnModelPropertyChanged();
            }
        }
        public bool PravnoLice
        {
            get => _model.PravnoLice;
            set
            {
                _model.PravnoLice = value;
                OnModelPropertyChanged();
            }
        }
        public bool FizickoLice
        {
            get => _model.FizickoLice;
            set
            {
                _model.FizickoLice = value;
                OnModelPropertyChanged();
            }
        }

        public string PunNaziv
        {
            get
            {
                if (_model.PravnoLice == true)
                {
                    return $"{_model.Sifra} - {_model.Naziv}";
                } else
                {
                    return $"{_model.Sifra} - {_model.Ime} {_model.Prezime}";
                }
            }
        }
    }
}
