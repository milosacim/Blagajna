using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MivexBlagajna.UI.Wrappers
{
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        public T Model { get; }
        public ModelWrapper(T model)
        {
            Model = model;
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName);
        }

        private void ValidatePropertyInternal(string propertyName)
        {
            ClearErrors(propertyName);

            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
}

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
