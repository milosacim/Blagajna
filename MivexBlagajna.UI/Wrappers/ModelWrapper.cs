using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MivexBlagajna.UI.Wrappers
{
    public abstract class ModelWrapper<T> : NotifyDataErrorInfoBase, IEditableObject
    {
        public T Model { get; }

        public ModelWrapper(T model)
        {
            Model = model;
        }
        protected virtual TValue GetValue<TValue>([CallerMemberName] string? propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string? propertyName = null)
        {
            if (value != null)
            {
                typeof(T).GetProperty(propertyName).SetValue(Model, value);
                ValidatePropertyInternal(propertyName, value);
                OnModelPropertyChanged(propertyName);
            }
        }
        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);
            ValidateDataAnnotations(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }
        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            var context = new ValidationContext(Model) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(currentValue, context, results);

            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        protected virtual IEnumerable<string>? ValidateProperty(string propertyName)
        {
            return null;
        }

        public abstract void BeginEdit();
        public abstract void CancelEdit();
        public abstract void EndEdit();
    }
}
