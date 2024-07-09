using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Emp_Demo.Models
{

    public class EmployeeinfoViewModel
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(100, ErrorMessage = "Employee Name cannot be longer than 100 characters")]
        public string EmployeeName { get; set; }
   
       
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [Phone(ErrorMessage = "Invalid Contact Number")]
        [RegularExpression(@"^\+?\d{0,3}[\s-]?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{4}$", ErrorMessage = "Invalid Contact Number")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = " Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}