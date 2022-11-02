namespace MivexBlagajna.UI.EventArgs
{
    public class SelectedKomitentArgs
    {
        public readonly int? oldId;
        public readonly int newid;
        public readonly bool isSelected;

        public SelectedKomitentArgs(int newid, bool isSelected, int? oldId = null)
        {
            this.oldId = oldId;
            this.newid = newid;
            this.isSelected = isSelected;
        }
    }
}