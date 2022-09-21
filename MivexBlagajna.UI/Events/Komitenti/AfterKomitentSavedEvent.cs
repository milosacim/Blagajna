using MivexBlagajna.Data.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Events.Komitenti
{
    public class AfterKomitentSavedEvent : PubSubEvent<AfterKomitentSavedEventArgs>
    {
    }

    public class AfterKomitentSavedEventArgs
    {
        public int Id { get; set; }
        public string PunNaziv { get; set; }
        public bool PravnoLice { get; set; }
        public bool FizickoLice { get; set; }

        public string MestoTroska { get; set; }
    }
}
