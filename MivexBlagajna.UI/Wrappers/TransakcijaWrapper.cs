using MivexBlagajna.Data.Models;
using System;

namespace MivexBlagajna.UI.Wrappers
{
    public class TransakcijaWrapper : ModelWrapper<Transakcija>
    {
        private bool _isEditable;

        public TransakcijaWrapper(Transakcija transakcija, bool isEditable) : base(transakcija)
        {
            _isEditable = isEditable;
        }

        public int Id { get { return Model.Id; } }

        public int Komitent_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Komitent Komitent
        {
            get { return GetValue<Komitent>(); }
            set { SetValue(value); }
        }

        public int MestoTroska_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public MestoTroska MestoTroska
        {
            get { return GetValue<MestoTroska>(); }
            set { SetValue(value); }
        }

        public int Konto_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Konto Konto
        {
            get { return GetValue<Konto>(); }
            set { SetValue(value); }
        }

        public DateTime Datum
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public string VrstaNaloga
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Nalog
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Opis
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
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


        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnModelPropertyChanged(); }
        }

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
