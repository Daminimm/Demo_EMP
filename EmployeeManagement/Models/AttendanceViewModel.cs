using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using EmployeeManagement.Enums;

namespace EmployeeManagement.Models
{
    public class AttendanceViewModel
    {
        
            public int AttendanceId { get; set; }
            public int EmployeeId { get; set; }
            public DateTime AttendanceDate { get; set; }
            public DateTime Timestamp { get; set; }
            public EntryType EntryType { get; set; }

   
    }
}