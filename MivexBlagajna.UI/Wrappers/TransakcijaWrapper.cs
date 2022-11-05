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
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public Komitent Komitent
        {
            get { return GetValue<Komitent>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int MestoTroska_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public MestoTroska MestoTroska
        {
            get { return GetValue<MestoTroska>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int Konto_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public Konto Konto
        {
            get { return GetValue<Konto>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public DateTime Datum
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int VrsteNaloga_Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public VrsteNaloga VrstaNaloga
        {
            get { return GetValue<VrsteNaloga>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public string Nalog
        {
            get { return GetValue<string>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public string Opis
        {
            get { return GetValue<string>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public decimal Uplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public decimal Isplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnModelPropertyChanged(); }
        }

        public override void BeginEdit()
        {
            _isEditable = true;
        }

        public override void CancelEdit()
        {
            _isEditable = false;
        }

        public override void EndEdit()
        {
            _isEditable = false;
        }
    }
}
