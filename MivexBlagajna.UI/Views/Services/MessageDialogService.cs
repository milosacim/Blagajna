using System.Windows;

namespace MivexBlagajna.UI.Views.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOKCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK ? MessageDialogResult.Potvrdi : MessageDialogResult.Otkazi;
        }
    }

    public enum MessageDialogResult { Potvrdi, Otkazi }

}
