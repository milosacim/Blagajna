namespace MivexBlagajna.UI.Views.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOKCancelDialog(string text, string title);
    }
}