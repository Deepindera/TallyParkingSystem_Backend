using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TallyParkingSystem.Model
{
    public class ExitTimeValidateAttribute : ValidationAttribute
    {
        private readonly string _entryTimePropertyName;

        public ExitTimeValidateAttribute(string propertyNameToCheck)
        {
            _entryTimePropertyName = propertyNameToCheck;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.ObjectType.GetProperty(_entryTimePropertyName);            
            var propertyValue = propertyName.GetValue(validationContext.ObjectInstance);
            
            var result = ConvertToDateTime(propertyValue) >= ConvertToDateTime(value) ?
                new ValidationResult($"{_entryTimePropertyName} cannot be greater than exit time", new List<string>() { "ExitTime" }) :
                ValidationResult.Success;

            return result;
        }

        private DateTime ConvertToDateTime(Object value)
        {
            DateTime convertedDate;

            try
            {
                convertedDate = Convert.ToDateTime(value);

            }
            catch (FormatException e)
            {
                throw;
            }
            catch (InvalidCastException e)
            {
                throw;
            }

            return convertedDate;
        }
    }
}
