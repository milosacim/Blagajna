namespace MivexBlagajna.UI.EventArgs
{
    public class SavedMestoTroskaArgs
    {
        public readonly int id;
        public readonly string prefix;
        public readonly string naziv;
        public readonly int? nadId;

        public SavedMestoTroskaArgs(int id, string prefix, string naziv, int? nadId)
        {
            this.id = id;
            this.prefix = prefix;
            this.naziv = naziv;
            this.nadId = nadId;
        }
    }
}