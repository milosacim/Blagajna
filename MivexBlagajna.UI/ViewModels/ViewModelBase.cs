using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class ViewModelBase :  INotifyPropertyChanged
    {
        #region Implementacija INotifyPropertyChanged interfejsa
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnModelPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region LoadAsync definicija
        public virtual Task LoadAsync() => Task.CompletedTask;
        #endregion
    }
}
