using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska
    {
        public int Id { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        [StringLength(128)]
        public string Naziv { get; set; }
    }
}
