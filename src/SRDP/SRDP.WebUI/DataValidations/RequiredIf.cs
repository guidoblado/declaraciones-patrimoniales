using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.DataValidations
{
    public class RequiredIf : ValidationAttribute, IClientValidatable
    {

        private readonly string propertyNameToCheck;

        public RequiredIf(string propertyNameToCheck)
        {
            this.propertyNameToCheck = propertyNameToCheck;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.ObjectType.GetProperty(propertyNameToCheck);
            if (propertyName == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { propertyNameToCheck }));

            var propertyValue = propertyName.GetValue(validationContext.ObjectInstance, null) as string;

            if (value != null)
                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
           
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = string.Format(ErrorMessageString, metadata.GetDisplayName()),
                ValidationType = "requiredif"
            };
            rule.ValidationParameters.Add("propertynametocheck", propertyNameToCheck);
           
            yield return rule;
        }
    }
}