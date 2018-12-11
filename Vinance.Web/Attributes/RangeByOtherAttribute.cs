using System;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Attributes
{
    using Contracts.Enumerations;   

    public class RangeByOtherAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly int _min;
        private readonly int _max;

        public RangeByOtherAttribute(string comparisonProperty, int min, int max)
        {
            _comparisonProperty = comparisonProperty;
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (int)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var comparisonValue = (CategoryType)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue != CategoryType.Expense)
            {
                return currentValue == 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            }

            if (currentValue >= _min && currentValue <= _max)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}