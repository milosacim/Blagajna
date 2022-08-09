using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class ViewModelBase : NotificationObject //INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public virtual Task LoadAsync() => Task.CompletedTask;

    }
}
