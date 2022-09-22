using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.Data.Models
{
    public class Transakcija
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Komitent Komitent { get; set; }
        public Konto Konto { get; set; }
        public string Nalog { get; set; }
        public string Opis { get; set; }
        public bool Opravdan { get; set; }
        public bool Neopravndan { get; set; }
        public decimal Uplata { get; set; }
        public decimal Isplata { get; set; }
    }
}
