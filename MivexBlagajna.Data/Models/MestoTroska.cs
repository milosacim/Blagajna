using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska
    {
        [Key]
        public int MestoTroska_Id { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        [StringLength(128)]
        public string Naziv { get; set; }
        public ICollection<NosilacTroska> NosiociTroska { get; set; }

    }
}
