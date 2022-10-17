using MivexBlagajna.Data.Models;

namespace MivexBlagajna.UI.Wrappers
{
    public class MestoTroskaWrapper : ModelWrapper<MestoTroska>
    {
        public MestoTroskaWrapper(MestoTroska mestoTroska) : base(mestoTroska)
        {
            Prefix = mestoTroska.Prefix;
            Naziv = mestoTroska.Naziv;
            Nivo = mestoTroska.Nivo;

            IsEditable = false;
        }

        public int Id { get { return Model.Id; } }
        public int Nadiredjeni_Id { get { return Model.NadredjenoMesto_Id; } }
        public string Prefix
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Naziv
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int Nivo
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public bool IsEditable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void BeginEdit()
        {
            throw new System.NotImplementedException();
        }

        public override void CancelEdit()
        {
            throw new System.NotImplementedException();
        }

        public override void EndEdit()
        {
            throw new System.NotImplementedException();
        }
    }
}
