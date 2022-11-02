namespace MivexBlagajna.UI.EventArgs
{
    public class MestoTroskaArgs
    {
        public int newid;
        public int? newNadId;
        public bool isSelected;
        public int? oldId;

        public MestoTroskaArgs(int newid, int? newNadId, bool isSelected, int? oldId = null)
        {
            this.newid = newid;
            this.newNadId = newNadId;
            this.isSelected = isSelected;
            this.oldId = oldId;
        }
    }
}