namespace MivexBlagajna.UI
{
    public interface IDockElement
    {
        string? Header { get; }
        DockState State { get; }
    }
}
