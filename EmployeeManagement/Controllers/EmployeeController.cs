using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Enums;
using System.Data.Entity;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
         Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();

        public ActionResult EmployeeDashboard(Employeeinfo MODEL)
        {
       
            int userId = GetLoggedInUserId();
            var employee = DbContext.Employeeinfoes.Where(e => e.Userlogin.UserId== userId).FirstOrDefault();
            if (employee != null)
            {
             
                ViewBag.EmployeeName = employee.EmployeeName;

                
                return View(new List<Employeeinfo> { employee });
            }
            //if (employee != null)
            //{

            //    ViewBag.EmployeeName = employee.EmployeeName;

            //     return View(employee);
            //}
            else
            {
              return HttpNotFound(); // Handle if employee not found
            }

        }
        //{
        //    int userId = GetLoggedInUserId();
        //    var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.UserId == userId);

        //    if (employee == null)
        //    {
        //        return HttpNotFound("Employee not found.");
        //    }

        //    var attendance = DbContext.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).ToList();



        //    return View();
        //}

        protected int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }
            throw new Exception("User ID not found in session.");
        }
    }

    //    public ActionResult EmployeeDashboard()
    //    {


    //        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();

    //        return View();
    //    }


    //    // GET: Attendance/PunchIn
    //    public ActionResult PunchIn()
    //    {
    //        return View();
    //    }

    //    // POST: Attendance/PunchIn
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult PunchIn(Attendance attendance)
    //    {
    //        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
    //        if (ModelState.IsValid)
    //        {
    //            attendance.EmployeeId = GetLoggedInEmployeeId();
    //            attendance.AttendanceDate = DateTime.Now.Date;
    //            attendance.Timestamp = DateTime.Now;
    //            attendance.EntryType = "PunchIn";
    //            DbContext.Attendances.Add(attendance);
    //            DbContext.SaveChanges();
    //            return RedirectToAction("EmployeeDashboard");
    //        }

    //        return View(attendance);
    //    }

    //    // GET: Attendance/PunchOut
    //    public ActionResult PunchOut()
    //    {
    //        return View();
    //    }

    //    // POST: Attendance/PunchOut
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult PunchOut(Attendance attendance)
    //    {
    //        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
    //        if (ModelState.IsValid)
    //        {
    //            attendance.EmployeeId = GetLoggedInEmployeeId();
    //            attendance.AttendanceDate = DateTime.Now.Date;
    //            attendance.Timestamp = DateTime.Now;
    //            attendance.EntryType = "PunchOut";
    //            DbContext.Attendances.Add(attendance);
    //            DbContext.SaveChanges();
    //            return RedirectToAction("EmployeeDashboard");
    //        }

    //        return View(attendance);
    //    }

    //    private int GetLoggedInEmployeeId()
    //    {
    //        // Implement this method to retrieve the currently logged-in employee's ID
    //        // For example, you might retrieve it from the session or authentication ticket
    //        return 1; // Placeholder value
    //    }



    //}
} 

 