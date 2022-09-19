using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public class UplateIsplateViewModel : ViewModelBase, IDockElement
    {

        private readonly DockState _dockState;
        private readonly string _header;

        public UplateIsplateViewModel(
            )
        {
            _dockState = DockState.Document;
            _header = "Uplate / Isplate";
        }

        public DockState State
        {
            get { return _dockState; }
            set {  }
        }

        public string Header
        {
            get { return _header; }
            set {  }
        }

        
    }
}
