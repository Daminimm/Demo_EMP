using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class CustomContactValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Not required, adjust as per your business rules

            string contact = value.ToString();

            // Regular expression for phone number format
            string phoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            // Validate using regular expression and numeric check
            if (!Regex.IsMatch(contact, phoneNumberPattern) || !IsDigitsOnly(contact))
            {
                return false;
            }

            return true;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
