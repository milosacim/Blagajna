using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Events.Mesta_Troska
{
    public class AfterMestoTroskaSavedEvent : PubSubEvent<AfterMestoTroskaSavedArgs>
    {
    }

    public class AfterMestoTroskaSavedArgs
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public int Nivo { get; set; }
        public int NadredjenoMesto_Id { get; set; }
    }
}
