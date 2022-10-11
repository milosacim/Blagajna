using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Wrappers
{
    public class MestoTroskaWrapper : ModelWrapper<MestoTroska>
    {
        public MestoTroskaWrapper(MestoTroska mestoTroska) : base(mestoTroska)
        {
            Prefix = mestoTroska.Prefix;
            Naziv = mestoTroska.Naziv;
            Nivo = mestoTroska.Nivo;
        }

        public int Id { get { return Model.Id; } }
        public int Nadiredjeni_Id { get { return Model.NadredjenoMesto_Id; } }
        public string Prefix
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Naziv
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int Nivo
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }


    }
}
