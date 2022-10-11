using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MivexBlagajna.UI.ViewModels
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnObjectPropertyChanged(object oldvalue, object newvalue, [CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedExtendedEventArgs(propertyName, oldvalue, newvalue));
            }
        }
    }

    public class PropertyChangedExtendedEventArgs : PropertyChangedEventArgs
    {
        public PropertyChangedExtendedEventArgs(string? propertyName, object oldValue, object newValue) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        public virtual object OldValue { get; private set; }
        public virtual object NewValue { get; private set; }
    }
}
