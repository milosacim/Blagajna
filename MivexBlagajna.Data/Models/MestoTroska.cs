using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska : ISoftDeletable
    {
        [Key]
        public int Id { get; set;}

        [Required]
        public string Prefix { get; set; }

        [Required]
        public string Naziv { get; set; }
        public int? NadredjenoMesto_Id { get; set; }
        public virtual MestoTroska RoditeljMestoTroska { get; set; }
        public virtual ICollection<MestoTroska> DecaMestoTroska { get; set; }
        public virtual ICollection<Komitent> Komitenti { get; set; }
        public virtual ICollection<Transakcija> Transakcije { get; set; }
        public bool Obrisano { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Prefix, Naziv);
        }
    }
}
