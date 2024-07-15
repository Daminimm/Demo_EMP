using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emp_Demo.CustomValidation
{
    public class EmployeeRoleValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string role = value as string;

            // Check if the role is empty or null
            if (string.IsNullOrWhiteSpace(role))
            {
                return true; // Return true because it's not mandatory
            }

            // Check if the role is specifically "Employee"
            if (role.Trim().Equals("Employee", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field can only be filled if the role is specified as 'Employee'.";
        }
    }
}