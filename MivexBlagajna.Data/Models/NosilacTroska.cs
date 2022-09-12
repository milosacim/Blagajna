using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MivexBlagajna.Data.Models
{
    public class NosilacTroska
    {
        [Key]
        public int NosilacTroska_id { get; set; }
        [Required]
        public string Sifra { get; set; }
        [StringLength(128)]
        public string Naziv { get; set; }
        [Required]
        public int Nivo { get; set; }
        [Required]
        public int MestoTroska_Id { get; set; }
        public MestoTroska MestoTroska { get; set; }
    }
}
