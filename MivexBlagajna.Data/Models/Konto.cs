using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class Konto
    {
        public Konto()
        {
            Transakcije = new List<Transakcija>();
        }
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public virtual ICollection<Transakcija> Transakcije { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Naziv);
        }
    }
}
