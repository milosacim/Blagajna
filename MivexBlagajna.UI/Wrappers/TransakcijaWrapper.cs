using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Wrappers
{
    public class TransakcijaWrapper : ModelWrapper<Transakcija>
    {
        public TransakcijaWrapper(Transakcija transakcija) : base(transakcija)
        {
            Opis = transakcija.Opis;
            Uplata = transakcija.Uplata;
            Isplata = transakcija.Isplata;
        }

        public int Id { get { return Model.Id; } }

        public int Komitent_Id
        {
            get { return Model.Komitent_Id; }
        }

        public int MestoTroska_Id
        {
            get { return Model.MestoTroska_Id; }
        }

        public int Konto_Id
        {
            get { return Model.Konto_Id; }
        }

        public DateTime Datum
        {
            get { return Model.Datum; }
        }

        public string Nalog
        {
            get { return Model.Nalog; }
        }

        public string Opis
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool? Opravdan
        {
            get { return Model.Opravdan; }
        }

        public bool? Neopravdan
        {
            get { return Model.Opravdan; }
        }

        public decimal Uplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal Isplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
