﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Models;
using Emp_Demo.Enums;
using System.Data.Entity;


namespace Emp_Demo.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        //public ActionResult EmployeeDashboard()
        //{
        //    int userId = GetLoggedInUserId();
        //    var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.Userlogin.UserId == userId);

        //    if (employee != null)
        //    {
        //        ViewBag.EmployeeName = employee.EmployeeName;
        //        return View(new List<Employeeinfo> { employee });
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }
        //}
        public ActionResult EmployeeDashboard()
        {
            int userId = GetLoggedInUserId(); // Replace with your method to get logged in user ID
            var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.Userlogin.UserId == userId);

            if (employee != null)
            {
                var attendances = DbContext.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).ToList();
                var lastAttendance = attendances.OrderByDescending(a => a.Timestamp).FirstOrDefault();
                ViewBag.EmployeeName = employee.EmployeeName;

                var model = new AttendanceViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    AttendanceDate = DateTime.Today,
                    Timestamp = DateTime.Now,
                   
                    Attendances = attendances

                };
                if (lastAttendance == null || lastAttendance.EntryType == EntryTypeEnum.PunchOut.ToString())
                {
                    model.EntryType = EntryTypeEnum.PunchIn; // Set EntryType to PunchIn enum value
                }
                else if (lastAttendance.EntryType == EntryTypeEnum.PunchIn.ToString())
                {
                    model.EntryType = EntryTypeEnum.PunchOut; // Set EntryType to PunchOut enum value
                }

                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PunchIn(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendance = new Attendance
                {
                    EmployeeId = model.EmployeeId,
                    AttendanceDate = DateTime.Today, // Ensure AttendanceDate is also of type DateTime
                    Timestamp = DateTime.Now, // or DateTime.UtcNow
                    EntryType = EntryTypeEnum.PunchIn.ToString()
                };

                DbContext.Attendances.Add(attendance);
                DbContext.SaveChanges();

                return RedirectToAction("EmployeeDashboard"); // Redirect to employee dashboard
            }

            return View(model); // Return view with model if validation fails
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PunchOut(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendance = new Attendance
                {
                    EmployeeId = model.EmployeeId,
                    AttendanceDate = DateTime.Today, // Ensure AttendanceDate is also of type DateTime
                    Timestamp = DateTime.Now, // or DateTime.UtcNow
                    EntryType = EntryTypeEnum.PunchOut.ToString()
                };

                DbContext.Attendances.Add(attendance);
                DbContext.SaveChanges();

                return RedirectToAction("EmployeeDashboard"); // Redirect to employee dashboard
            }

            return View(model); // Return view with model if validation fails
        }


        // GET: Employee/AttendanceReport
        public ActionResult AttendanceReport()
        {
            int userId = GetLoggedInUserId(); // Replace with your method to get logged in user ID
            var attendances = DbContext.Attendances.Where(a => a.EmployeeId == userId).ToList();
            return View(attendances);
        }


    }
}