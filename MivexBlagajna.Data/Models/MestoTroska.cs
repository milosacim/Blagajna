using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska : ISoftDeletable
    {
        public MestoTroska()
        {
            Komitenti = new List<Komitent>();
            Transakcije = new List<Transakcija>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Prefix { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Required]
        public int Nivo { get; set; }
        public int NadredjenoMesto_Id { get; set; }
        public bool Obrisano { get; set; }
        public ICollection<Komitent>? Komitenti { get; set; }
        public ICollection<Transakcija> Transakcije { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Prefix, Naziv);
        }
    }
}
