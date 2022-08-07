using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class Komitent
    {
        public int Id { get; set; }

        [Required]
        public int Sifra { get; set; }
        public string? Naziv { get; set; }
        public string? Naziv2 { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? PostanskiBroj { get; set; }
        public string? Mesto { get; set; }
        public string? Adresa { get; set; }
        public string? KontaktOsoba { get; set; }
        public string? Telefon { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }
    }
}
