namespace MivexBlagajna.Data.Models.Lookups
{
    public class LookupKomitent
    {
        public int Id { get; set; }
        public string PunNaziv { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }
        public string PostanskiBroj { get; set; }
        public string Mesto { get; set; }
        public string Adresa { get; set; }
        public string Kontakt { get; set;}
    }
}
