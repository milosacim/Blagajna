using System;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        private string _header;
        private DockState _state;
        public MestaTroskaViewModel(string header = "Mesta troska",
            DockState state = DockState.Document)
        {
            _header = header;
            _state = state;
        }

        public string? Header
        {
            get { return _header; }
            set { }
        }
        public DockState State
        {
            get { return _state; }
            set { }
        }
    }
}