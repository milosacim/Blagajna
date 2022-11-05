using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class Transakcija
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Broj { get; set; }
        public string Nalog { get; set; }
        public string? Opis { get; set; }
        public decimal Uplata { get; set; }
        public decimal Isplata { get; set; }
        public int VrsteNaloga_Id { get; set; }
        public int Komitent_Id { get; set; }
        public int MestoTroska_Id { get; set; }
        public int Konto_Id { get; set; }

        public virtual Komitent Komitent { get; set; }
        public virtual MestoTroska MestoTroska { get; set; }
        public virtual Konto Konto { get; set; }
        public virtual VrsteNaloga VrstaNaloga { get; set; }
    }
}
