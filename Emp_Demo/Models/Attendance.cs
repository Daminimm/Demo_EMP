//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emp_Demo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string EntryType { get; set; }
    
        public virtual Employeeinfo Employeeinfo { get; set; }
    }
}
