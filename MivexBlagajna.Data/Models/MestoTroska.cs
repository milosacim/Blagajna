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
            if (RoditeljMestoTroska != null)
            {
                if (RoditeljMestoTroska.RoditeljMestoTroska != null)
                {
                    return $"{RoditeljMestoTroska.RoditeljMestoTroska?.Prefix} {RoditeljMestoTroska.RoditeljMestoTroska?.Naziv} / {RoditeljMestoTroska.Prefix} {RoditeljMestoTroska.Naziv} / {Prefix} {Naziv}";
                }
                else if (RoditeljMestoTroska != null)
                {
                    return $"{RoditeljMestoTroska.Prefix} {RoditeljMestoTroska.Naziv} / {Prefix} {Naziv}";
                }
                else
                {
                    return $"{Prefix} {Naziv}";
                }
            }
            else
            {
                return $"{Prefix} {Naziv}";
            }
        }
    }
}
