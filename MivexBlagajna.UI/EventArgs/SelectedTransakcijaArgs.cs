using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.EventArgs
{
    public class SelectedTransakcijaArgs
    {
        public readonly int id;

        public SelectedTransakcijaArgs(int id)
        {
            this.id = id;
        }
    }
}
