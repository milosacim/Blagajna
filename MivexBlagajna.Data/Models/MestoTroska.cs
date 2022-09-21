using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska
    {
        public MestoTroska()
        {
            Komitenti = new List<Komitent>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int Nivo { get; set; }
        [Required]
        public int NadredjenoMesto_Id { get; set; }
        public ICollection<Komitent>? Komitenti { get; set; }
    }
}
