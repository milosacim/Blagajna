using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;

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
            set { SetValue(value); OnPropertyChanged(); }
        }

        public int? MestoTroska_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public MestoTroska MestoTroska
        {
            get { return GetValue<MestoTroska>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public int? Konto_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public Konto Konto
        {
            get { return GetValue<Konto>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public DateTime Datum
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public int? VrsteNaloga_Id
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public VrsteNaloga VrstaNaloga
        {
            get { return GetValue<VrsteNaloga>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public string Nalog
        {
            get { return GetValue<string>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public string Opis
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value); 
                OnPropertyChanged();
            }
        }

        public decimal Uplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public decimal Isplata
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); OnPropertyChanged(); }
        }

        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnPropertyChanged(); }
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

        protected override IEnumerable<string>? ValidateProperty(string propertyName)
        {
            switch(propertyName)
            {
                case nameof(Uplata):
                    if(Uplata > 0 && Isplata > 0)
                    {
                        yield return $"Isplata je već uneta.";
                    }
                    else
                    {
                        ClearErrors(nameof(Isplata));
                        break;
                    }
                    break;

                case nameof(Isplata):
                    if (Uplata > 0 && Isplata > 0)
                    {
                        yield return $"Uplata je već uneta.";
                    }
                    else
                    {
                        ClearErrors(nameof(Isplata));
                        break;
                    }
                    break;
            }
        }
    }
}
