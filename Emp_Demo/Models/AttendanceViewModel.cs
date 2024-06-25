using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emp_Demo.Enums;

namespace Emp_Demo.Models
{
    public class AttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public System.DateTime Timestamp { get; set; }
        public EntryTypeEnum EntryType { get; set; }

        public virtual Employeeinfo Employeeinfo { get; set; }
        
        public List<Attendance> Attendances { get; set; }
   



    }
}