using System;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public class MestaTroskaNavigationItemViewModel : ViewModelBase
    {
        private string? _naziv;
        private string? _sifra;
        private int? _nadredjeniId;
        private bool _isSelected;

        public MestaTroskaNavigationItemViewModel(int id, string sifra, string naziv, int? nadredjeni)
        {
            Id = id;
            Sifra = sifra;
            Naziv = naziv;
            Nadredjeni_Id = nadredjeni;

            _isSelected = false;
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

        public int? Nadredjeni_Id
        {
            get { return _nadredjeniId; }
            set { 
                var oldValue = _nadredjeniId;
                _nadredjeniId = value;
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
            _naziv = null;
            _sifra = null;
            _nadredjeniId = null;

            base.Dispose(disposing);
        }
    }
}
