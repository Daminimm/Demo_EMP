using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class CustomEmployeeNameFormatAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string employeeName = value.ToString();

                // Check if the name starts with a capital letter and has only lowercase letters afterwards
                if (!IsEmployeeNameValid(employeeName))
                {
                    return new ValidationResult("Employee name must start with a capital letter followed by lowercase letters.");
                }
            }

            return ValidationResult.Success;
        }

        private bool IsEmployeeNameValid(string employeeName)
        {
            // Ensure the first character is uppercase and all subsequent characters are lowercase
            if (string.IsNullOrEmpty(employeeName))
                return false;

            if (!char.IsUpper(employeeName[0]))
                return false;

            for (int i = 1; i < employeeName.Length; i++)
            {
                if (!char.IsLower(employeeName[i]))
                    return false;
            }

            return true;
        }
    }
}
