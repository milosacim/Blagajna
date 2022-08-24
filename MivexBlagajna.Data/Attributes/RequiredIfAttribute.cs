using System;
using System.ComponentModel.DataAnnotations;

namespace MivexBlagajna.Data.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private string ErrorMessage { get; set; }
        private bool DesiredValue { get; set; }

        public RequiredIfAttribute(string propertyName, string errorMessage, bool desiredValue)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            DesiredValue = desiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value != null) { return ValidationResult.Success; }

            var proprtyvalue = context.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(context.ObjectInstance);

            if ((bool)proprtyvalue != false)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
