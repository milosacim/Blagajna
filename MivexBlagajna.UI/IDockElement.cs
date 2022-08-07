using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI
{
    public interface IDockElement
    {
        string Header { get; set; }
        DockState State { get; set; }
    }
}
