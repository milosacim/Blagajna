namespace MivexBlagajna.UI.EventArgs
{
    public class KomitentSavedArgs
    {
        public readonly int id;
        public readonly string naziv;
        public readonly bool pravno;
        public readonly bool fizicko;
        public readonly string adresa;
        public readonly string postBroj;
        public readonly string mesto;
        public readonly string telefon;

        public KomitentSavedArgs(int id, string naziv, bool pravno, bool fizicko, string adresa, string postBroj, string mesto, string telefon)
        {
            this.id = id;
            this.naziv = naziv;
            this.pravno = pravno;
            this.fizicko = fizicko;
            this.adresa = adresa;
            this.postBroj = postBroj;
            this.mesto = mesto;
            this.telefon = telefon;
        }
    }
}