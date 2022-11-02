namespace MivexBlagajna.UI.EventArgs
{
    public class KomitentSavedArgs
    {
        public readonly int id;
        public readonly string naziv;
        public readonly bool pravno;
        public readonly bool fizicko;
        public KomitentSavedArgs(int id, string naziv, bool pravno, bool fizicko)
        {
            this.id = id;
            this.naziv = naziv;
            this.pravno = pravno;
            this.fizicko = fizicko;
        }
    }
}