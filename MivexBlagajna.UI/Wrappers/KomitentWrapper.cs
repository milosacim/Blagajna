using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;

namespace MivexBlagajna.UI.Wrappers
{

    public class KomitentWrapper : ModelWrapper<Komitent>
    {
        #region Konstruktor
        public KomitentWrapper(Komitent komitent) : base(komitent)
        {
            
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
        public string Naziv
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
        public string Ime
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
        public string Prezime
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
        public string Jmbg
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
        public string Pib
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
        public string MaticniBroj
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
                } else
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
        public bool IsEditable
        {
            get => GetValue<bool>();
            set
            {
                SetValue(value);
            }
        }
        #endregion
    }
}
