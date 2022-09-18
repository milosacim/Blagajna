using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Events
{
    public class OnOpenMestoTroskaDetailsEvent : PubSubEvent<OnOpenMestoTroskaDetailsArgs>
    {

    }

    public class OnOpenMestoTroskaDetailsArgs
    {
        public int Id { get; set; }
        public int NadredjenoMesto_Id { get; set; }
    }
}
