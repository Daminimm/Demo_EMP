using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Emp_Demo.CustomValidation;


namespace Emp_Demo.Models
{

    public class EmployeeinfoViewModel
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(100, ErrorMessage = "Employee Name cannot be longer than 100 characters")]
        [CustomEmployeeNameFormat(ErrorMessage = "Employee name must start with a capital letter followed by lowercase letters.")]
        public string EmployeeName { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter your email address")]
        [CustomEmailFormat(ErrorMessage = "Email must end with '@gmail.com', start with a lowercase letter, and contain at least one digit.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Contact is required")]
        [MaxLength(10)]
        [CustomContactValidation(ErrorMessage = "Contact must be numeric and in valid phone number format (e.g., 123-456-7890).")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid numeric value")]
        public string Contact { get; set; }


        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = " Username is required")]
        [CustomUsernameValidation]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        [Required(ErrorMessage = "Password is required")]
        //[CustomPassword(8, ErrorMessage = "Password must be at least 8 characters long, contain at least one digit, and one special character.")]
        public string Password { get; set; }
        [EmployeeRoleValidation(ErrorMessage = "The Role field can only be filled if the role is specified as 'Employee'.")]
        public string Role { get; set; }
        public byte[] Image { get; set; }
        public HttpPostedFileBase user_image_data { get; set; }

    }   
}
