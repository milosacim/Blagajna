using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.Data.Models.UI_Models
{
    public class Workspace
    {
        private string _header;
        private DockState _state;

        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public DockState State
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}
