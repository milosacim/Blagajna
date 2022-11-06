using MivexBlagajna.Data.Models;
namespace MivexBlagajna.UI.Wrappers
{

    public class KomitentWrapper : ModelWrapper<Komitent>
    {
        private string _punNaziv;
        private bool _isEditable;
        private bool _isPravnoLiceEditable;
        private bool _isFizickoLiceEditable;

        #region Konstruktor
        public KomitentWrapper(Komitent komitent, bool isEditable, bool isPravnoLiceEditable, bool isFizickoLiceEditable) : base(komitent)
        {
            _isEditable = isEditable;
            _isPravnoLiceEditable = isPravnoLiceEditable;
            _isFizickoLiceEditable = isFizickoLiceEditable;
        }

        #endregion

        #region Properties
        // Propserties - value se setuje iz Komitent modela koji ova klasa wrapuje
        public int Id { get { return Model.Id; } }
        public int Sifra
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public string? Naziv
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string Naziv2
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string? Ime
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string? Prezime
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string? Jmbg
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string PostanskiBroj
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string? Pib
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string? MaticniBroj
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string Mesto
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string Adresa
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string KontaktOsoba
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public string Telefon
        {
            get
            {
                return GetValue<string>();
            }

            set
            {
                SetValue(value);
            }
        }
        public bool PravnoLice
        {
            get
            {
                return GetValue<bool>();
            }

            set
            {
                if (true == value)
                {
                    FizickoLice = false;
                    SetValue(value);
                }
                else
                {
                    SetValue(value);
                }
            }
        }
        public bool FizickoLice
        {
            get
            {
                return GetValue<bool>();
            }

            set
            {
                if (true == value)
                {
                    PravnoLice = false;
                    SetValue(value);
                }
                else
                {
                    SetValue(value);
                }
            }
        }
        public int? MestoTroska_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public MestoTroska? MestoTroska
        {
            get { return GetValue<MestoTroska?>(); }
            set { SetValue(value); }
        }

        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnModelPropertyChanged(); }
        }
        public bool IsPravnoLiceEditable
        {
            get { return _isPravnoLiceEditable; }
            set { _isPravnoLiceEditable = value; OnModelPropertyChanged(); }
        }
        public bool IsFizickoLiceEditable
        {
            get { return _isFizickoLiceEditable; }
            set { _isFizickoLiceEditable = value; OnModelPropertyChanged(); }
        }


        public string PunNaziv
        {
            get { return _punNaziv; }
            set
            {
                if (PravnoLice == true)
                {
                    _punNaziv = Sifra.ToString() + " - " + Naziv;
                }
                else
                {
                    _punNaziv = Sifra.ToString() + " - " + Ime + " " + Prezime;
                }
                OnModelPropertyChanged();
            }
        }

        public override void BeginEdit()
        {

            if (PravnoLice == FizickoLice)
            {
                IsEditable = false;
                IsPravnoLiceEditable = false;
                IsFizickoLiceEditable = false;
            }

            else
            {
                if (PravnoLice == true)
                {
                    IsEditable = true;
                    IsPravnoLiceEditable = true;
                    IsFizickoLiceEditable = false;
                }
                else
                {
                    IsEditable = true;
                    IsFizickoLiceEditable = true;
                    IsPravnoLiceEditable = false;
                }
            }
        }
        public override void CancelEdit()
        {
            IsEditable = false;
            IsFizickoLiceEditable = false;
            IsPravnoLiceEditable = false;
        }
        public override void EndEdit()
        {
            IsEditable = false;
            IsFizickoLiceEditable = false;
            IsPravnoLiceEditable = false;
        }

        #endregion

        public override string ToString()
        {
            return string.Format($"{0}", Naziv);
        }
    }
}
