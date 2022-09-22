using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.Data.Models
{
    public class Transakcija
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Nalog { get; set; }
        public string Opis { get; set; }
        public bool? Opravdan { get; set; }
        public bool? Neopravndan { get; set; }
        public decimal Uplata { get; set; }
        public decimal Isplata { get; set; }

        public int Komitent_Id { get; set; }
        public int MestoTroska_Id { get; set; }
        public int Konto_Id { get; set; }

        public Komitent Komitent { get; set; }
        public MestoTroska MestoTroska { get; set; }
        public Konto Konta { get; set; }
    }
}
