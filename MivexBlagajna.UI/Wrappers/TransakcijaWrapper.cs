using MivexBlagajna.Data.Models;
using System;

namespace MivexBlagajna.UI.Wrappers
{
    public class TransakcijaWrapper : ModelWrapper<Transakcija>
    {
        public TransakcijaWrapper(Transakcija transakcija) : base(transakcija)
        {
            Opis = transakcija.Opis;
            Uplata = transakcija.Uplata;
            Isplata = transakcija.Isplata;

            IsEditable = false;
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

        public bool IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public override void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public override void EndEdit()
        {
            throw new NotImplementedException();
        }
    }
}
