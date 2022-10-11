using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.Data
{
    public interface ISoftDeletable
    {
        public bool Obrisano { get; set; }
    }
}
