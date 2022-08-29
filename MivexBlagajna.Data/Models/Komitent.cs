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
        [RequiredIf(nameof(PravnoLice),"Morate uneti naziv komitenta!", true)]
        public string? Naziv { get; set; }
        [StringLength(100)]
        public string? Naziv2 { get; set; }
        [RequiredIf(nameof(FizickoLice), "Morate uneti ime komitenta!", true)]
        public string? Ime { get; set; }
        [RequiredIf(nameof(FizickoLice), "Morate uneti prezime komitenta!", true)]
        public string? Prezime { get; set; }
        [RequiredIf(nameof(FizickoLice), "Morate uneti JMBG komitenta!", true)]
        public string? Jmbg { get; set; }
        [StringLength(5)]
        public string? PostanskiBroj { get; set; }
        [RequiredIf(nameof(PravnoLice), "Morate uneti PIB komitenta!", true)]
        public string? Pib { get; set; }
        [RequiredIf(nameof(PravnoLice), "Morate uneti MB komitenta!", true)]
        public string? MaticniBroj { get; set; }
        public string? Mesto { get; set; }
        public string? Adresa { get; set; }
        [RequiredIf(nameof(PravnoLice), "Morate uneti kontakt osobu!", true)]
        public string? KontaktOsoba { get; set; }
        public string? Telefon { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }
    }
}
