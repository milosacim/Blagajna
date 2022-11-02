namespace MivexBlagajna.UI.EventArgs
{
    public class MestoTroskaDeletedArgs
    {
        public int id;
        public int? nadId;

        public MestoTroskaDeletedArgs(int id, int? nadId)
        {
            this.id = id;
            this.nadId = nadId;
        }
    }
}