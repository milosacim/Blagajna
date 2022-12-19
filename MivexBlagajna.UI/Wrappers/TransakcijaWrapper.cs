using MivexBlagajna.Data.Models;
using System;

namespace MivexBlagajna.UI.Wrappers
{
    public class TransakcijaWrapper : ModelWrapper<Transakcija>, IPrototype<TransakcijaWrapper>
    {
        private bool _isEditable;

        public TransakcijaWrapper(Transakcija transakcija, bool isEditable) : base(transakcija)
        {
            _isEditable = isEditable;
        }

        public int Id { get { return Model.Id; } }

        public int? Komitent_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public Komitent Komitent
        {
            get { return GetValue<Komitent>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int? MestoTroska_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public MestoTroska MestoTroska
        {
            get { return GetValue<MestoTroska>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int? Konto_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
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

        public int? VrsteNaloga_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
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
            set
            {
                SetValue(value); 
                OnModelPropertyChanged();
            }
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
            IsEditable = true;
        }

        public override void CancelEdit()
        {
            IsEditable = false;
        }

        public TransakcijaWrapper Clone()
        {
            return MemberwiseClone() as TransakcijaWrapper;
        }

        public TransakcijaWrapper DeepClone()
        {
            var newTransakcija = Clone();
            newTransakcija.Komitent = this.Komitent;
            newTransakcija.MestoTroska= this.MestoTroska;
            newTransakcija.Uplata = this.Uplata;
            newTransakcija.Isplata = this.Isplata;
            newTransakcija.Nalog = this.Nalog;
            newTransakcija.VrstaNaloga= this.VrstaNaloga;
            newTransakcija.Konto = this.Konto;
            newTransakcija.Opis= this.Opis;
            newTransakcija.Datum= this.Datum;

            return newTransakcija;
        }

        public override void EndEdit()
        {
            IsEditable = false;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
