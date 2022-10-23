﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Models
{
    public class MestoTroska : ISoftDeletable
    {
        public MestoTroska()
        {
            DecaMestoTroska = new List<MestoTroska>();
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
