using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnModelPropertyChanged(object? oldValue = null, object? newValue = null, [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedExtendedEventArgs(oldValue, newValue, propertyName));
        }

        public class PropertyChangedExtendedEventArgs : PropertyChangedEventArgs
        {
            public PropertyChangedExtendedEventArgs(object? oldValue, object? newValue, string? propertyName) : base(propertyName)
            {
                OldValue = oldValue;
                NewValue = newValue;
            }
            public virtual object? OldValue { get; private set; }
            public virtual object? NewValue { get; private set; }
        }
         
        public virtual Task LoadAsync() => Task.CompletedTask;

        public virtual void Dispose()
        {

        }
    }
}
