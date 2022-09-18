namespace MivexBlagajna.Data.Models
{
    public class LookupMestoTroska
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public int Nivo { get; set; }
        public int NadredjenoMesto_Id { get; set; }
    }

    public class NullMestoTroska : LookupMestoTroska
    {
        public new int? Id { get { return null; } }
        //public string? Sifra { get { return null; } }
        //public string? Naziv { get { return null; } }
        //public int? Nivo { get { return null; } }
        //public int? NadredjenoMesto_Id { get { return null; } }
    }
}
