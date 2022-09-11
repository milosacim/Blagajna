using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Events.Komitenti
{
    public class OnKomitentiNavigationFilterChangedEvent : PubSubEvent<FilterChangedArgs>
    {

    }
    public class FilterChangedArgs
    {
        public string NazivFilter { get; set; }
        public bool PravnoLiceFilter { get; set; }
        public bool FizickoLiceFilter { get; set; }
    }
}
