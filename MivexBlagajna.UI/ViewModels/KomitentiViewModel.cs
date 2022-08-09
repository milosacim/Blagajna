namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiViewModel : ViewModelBase, IDockElement
    {
        private string _header;
        private DockState _state;

        public KomitentiViewModel(string header, DockState state)
        {
            _header = header;
            _state = state;
        }

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
