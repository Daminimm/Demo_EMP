using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class CustomPasswordAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public CustomPasswordAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string password = value.ToString();

    
                if (password.Length < _minLength)
                {
                    return new ValidationResult($"The password must be at least {_minLength} characters long.");
                }

        
                if (!password.Any(char.IsDigit))
                {
                    return new ValidationResult("The password must contain at least one digit.");
                }

            
                if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    return new ValidationResult("The password must contain at least one special character.");
                }
            }

            return ValidationResult.Success;
        }
    }
}