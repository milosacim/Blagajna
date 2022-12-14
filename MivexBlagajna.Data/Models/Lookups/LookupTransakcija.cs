namespace MivexBlagajna.Data.Models.Lookups
{
    public class LookupTransakcija
    {
        public string Nalog { get; set; }
        public string VrstaNaloga { get; set; }
        public DateTime Datum { get; set; }
        public string Konto { get; set; }
        public int Sifra { get; set; }
        public string Komitent { get; set; }
        public string SifraMT { get; set; }
        public string Mesto { get; set; }
        public string Opis { get; set; }
        public decimal Uplata { get; set; }
        public decimal Isplata { get; set; }
        public decimal Saldo { get; set; }
    }
}
