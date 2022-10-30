using MivexBlagajna.Data.Models;

namespace MivexBlagajna.UI.Wrappers
{
    public class MestoTroskaWrapper : ModelWrapper<MestoTroska>
    {
        private bool _isEditable;

        public MestoTroskaWrapper(MestoTroska mestoTroska, bool isEditable) : base(mestoTroska)
        {
            _isEditable = isEditable;
            NadredjenoMesto_Id = mestoTroska.NadredjenoMesto_Id;
        }

        public int Id { get { return Model.Id; } }

        public int? NadredjenoMesto_Id
        {
            get { return GetValue<int?>(); }
            set
            {
                SetValue(value);
                OnModelPropertyChanged();
            }
        }

        public MestoTroska RoditeljMestoTroska
        {
            get { return GetValue<MestoTroska>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public string Prefix
        {
            get { return GetValue<string>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }
        public string Naziv
        {
            get { return GetValue<string>(); }
            set { SetValue(value); OnModelPropertyChanged(); }
        }

        public int Nivo
        {
            get { return GetValue<int>(); }
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
        public override void EndEdit()
        {
            IsEditable = false;
        }
    }
}
