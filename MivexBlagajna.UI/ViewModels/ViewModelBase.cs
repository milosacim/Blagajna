using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ViewModelBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
