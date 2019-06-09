using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace SRDP.WebUI.DataValidations
{
    public class RequiredOnDecimalPropertyValue : ValidationAttribute, IClientValidatable
    {
        private readonly string propertyNameToCheck;
        //private readonly decimal propertyValueToCheck;

        public RequiredOnDecimalPropertyValue(string propertyNameToCheck)
        {
            this.propertyNameToCheck = propertyNameToCheck;
            //this.propertyValueToCheck = propertyValueToCheck;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.ObjectType.GetProperty(propertyNameToCheck);
            if (propertyName == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { propertyNameToCheck }));
            Type propertyType = propertyName.PropertyType;

            var propertyValue = (decimal)propertyName.GetValue(validationContext.ObjectInstance, null);

            if (propertyValue > 0 && (value == null || ((string)value).Length == 0))
                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));

            return ValidationResult.Success;

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = string.Format(ErrorMessageString, metadata.GetDisplayName()),
                ValidationType = "requiredondecimalpropertyvalue"
            };
            rule.ValidationParameters.Add("nametocheck", propertyNameToCheck);
            yield return rule;
        }
    }
}