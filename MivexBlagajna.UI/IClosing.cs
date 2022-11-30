namespace MivexBlagajna.UI
{
    /// <summary>
    /// Executes when tab is closed
    /// </summary>
    /// <returns>Whether the tab should be closed by the caller</returns>
    public interface IClosing
    {
        bool OnClosing();
    }
}
