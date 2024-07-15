using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class CustomUsernameValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string username = value.ToString();
    

                // Check if username contains only alphanumeric characters
                if (!IsAlphanumeric(username))
                {
                    return new ValidationResult("Username must contain only letters and numbers.");
                }

                // Check if username length is between 3 and 20 characters
                if (username.Length < 3 || username.Length > 20)
                {
                    return new ValidationResult("Username must be between 3 and 20 characters long.");
                }
            }
            else
            {
                return new ValidationResult("Username is required.");
            }

            return ValidationResult.Success;
        }

        private bool IsAlphanumeric(string str)
        {
            return Regex.IsMatch(str, "^[a-zA-Z0-9]+$");
        }
    }
}
