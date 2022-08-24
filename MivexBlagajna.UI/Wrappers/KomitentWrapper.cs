using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;

namespace MivexBlagajna.UI.Wrappers
{

    public class KomitentWrapper : ModelWrapper<Komitent>
    {
        #region Konstruktor
        public KomitentWrapper(Komitent model) : base(model)
        {

        }

        #endregion

        #region Properties
        // Propserties - value se setuje iz Komitent modela koji ova klasa wrapuje

        public int Id { get => Model.Id; }
        public int Sifra
        {
            get => GetValue<int>();
            set
            {
                SetValue(value);
            }
        }
        public string Naziv
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string Naziv2
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string Ime
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string Prezime
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }

        public string Jmbg
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }

        public string PostanskiBroj
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }

        public string Pib
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }

        public string MaticniBroj
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }

        public string Mesto
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string Adresa
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string KontaktOsoba
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public string Telefon
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
            }
        }
        public bool PravnoLice
        {
            get => GetValue<bool>();
            set
            {
                SetValue(value);
            }
        }
        public bool FizickoLice
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
