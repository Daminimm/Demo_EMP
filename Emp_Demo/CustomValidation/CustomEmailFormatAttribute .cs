using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class CustomEmailFormatAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();

                // Check if email ends with "@gmail.com"
                if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult("Email must end with '@gmail.com'");
                }

                // Check if email starts with lowercase letters
                if (!Regex.IsMatch(email, @"^[a-z][a-z0-9._]*@[a-z0-9.-]+\.[a-z]{2,}$"))
                {
                    return new ValidationResult("Email must start with a lowercase letter and follow standard email format rules.");
                }

                // Check if email contains at least one digit
                if (!Regex.IsMatch(email, @"\d"))
                {
                    return new ValidationResult("Email must contain at least one digit.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
