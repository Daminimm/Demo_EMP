using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Enums;

namespace Emp_Demo.Models
{
    public class AttendanceViewModel
    {

        public int EmployeeId { get; set; }

        public int AttendanceId { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime AttendanceDate { get; set; }
  
        //[DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime Timestamp { get; set; }
        public EntryTypeEnum EntryType { get; set; }

        public virtual Employeeinfo Employeeinfo { get; set; }
        
        public List<Attendance> Attendances { get; set; }
   



    }
}