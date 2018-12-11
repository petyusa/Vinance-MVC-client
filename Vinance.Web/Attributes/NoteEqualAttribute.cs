using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Vinance.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NoteEqualAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _comparisonProperty;

        public NoteEqualAttribute(string comparisonProperty) : base(comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
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

            var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

            return currentValue == comparisonValue ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-notequalto", ErrorMessage);
            context.Attributes.Add("data-val-notequalto-other", _comparisonProperty);
        }
    }
}