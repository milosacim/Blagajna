using MivexBlagajna.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class Komitent
    { 
        public int Id { get; set; }
        [Required]
        public int Sifra { get; set; }

        [StringLength(80)]
        [RequiredIf(nameof(PravnoLice),"Morate uneti naziv komitenta", true)]
        public string? Naziv { get; set; }
        [StringLength(100)]
        public string? Naziv2 { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Jmbg { get; set; }
        [StringLength(5)]
        public string? PostanskiBroj { get; set; }
        public string? Pib { get; set; }
        public string? MaticniBroj { get; set; }
        public string? Mesto { get; set; }
        public string? Adresa { get; set; }
        public string? KontaktOsoba { get; set; }
        public string? Telefon { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }
    }
}
