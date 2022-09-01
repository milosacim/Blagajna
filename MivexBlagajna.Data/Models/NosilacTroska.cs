using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.Data.Models
{
    public class NosilacTroska
    {
        public int id { get; set; }
        [Required]
        public string Sifra { get; set; }
        [StringLength(128)]
        public string Naziv { get; set; }
        public MestoTroska mestoTroska { get; set; }
    }
}
