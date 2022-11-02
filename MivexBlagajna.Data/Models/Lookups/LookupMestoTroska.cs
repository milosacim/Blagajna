namespace MivexBlagajna.Data.Models.Lookups
{
    public class LookupMestoTroska
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public int? NadredjenoMesto_Id { get; set; }
    }
}
