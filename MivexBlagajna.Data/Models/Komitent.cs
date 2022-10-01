using MivexBlagajna.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class Komitent : ISoftDeletable
    {
        public Komitent()
        {
            Transakcije = new List<Transakcija>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int Sifra { get; set; }

        [RequiredIf(nameof(PravnoLice),"Obavezno polje!", true)]
        public string? Naziv { get; set; }

        public string? Naziv2 { get; set; }

        [RequiredIf(nameof(FizickoLice), "Obavezno polje!", true)]
        public string? Ime { get; set; }

        [RequiredIf(nameof(FizickoLice), "Obavezno polje!", true)]
        public string? Prezime { get; set; }

        [RequiredIf(nameof(FizickoLice), "Obavezno polje!", true)]
        public string? Jmbg { get; set; }

        public string? PostanskiBroj { get; set; }

        [RequiredIf(nameof(PravnoLice), "Obavezno polje!", true)]
        public string? Pib { get; set; }

        [RequiredIf(nameof(PravnoLice), "Obavezno polje!", true)]
        public string? MaticniBroj { get; set; }

        public string? Mesto { get; set; }
        public string? Adresa { get; set; }

        [RequiredIf(nameof(PravnoLice), "Obavezno polje!", true)]
        public string? KontaktOsoba { get; set; }
        public bool Obrisano { get; set; }

        public string? Telefon { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }
        public int? MestoTroska_id { get; set; }
        public virtual MestoTroska? MestoTroska { get; set; }
        public ICollection<Transakcija> Transakcije { get; set; }
    }
}
