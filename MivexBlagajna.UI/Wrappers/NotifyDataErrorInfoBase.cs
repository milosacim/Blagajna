using MivexBlagajna.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MivexBlagajna.UI.Wrappers
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsByPropertyName
             = new Dictionary<string, List<string>>();
        public bool HasErrors => _errorsByPropertyName.Any(); // true - ako _errorsByPropertyName sadrzi gresku
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null; // vraca gresku za propertyName ako postoji taj TKey
        }
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName)); // helper method koji podize ErrorsChanged Event
        }
        protected void AddError(string propertyName, string error)    // Ako _errorsByPropertyName ne sadrzi Tkey za propertyName onda se kreira nova Lista za taj Tkey,
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))   // zatim se proverava da li postoji ta greska i ako ne dodaje se u listu koja je Tvalue i podize ErrorsChanged event
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
        protected void ClearErrors(string propertyName)
        {

            if (_errorsByPropertyName.ContainsKey(propertyName)) // Ako _errorsByPropertyName sadrzi TKey za propertyName onda se brise i podize ErrorsChanged event
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
