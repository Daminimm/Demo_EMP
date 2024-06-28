using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Enums;
using Emp_Demo.Models;


namespace Emp_Demo.Controllers
{

    public class AdminController : Controller
    {
        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        // GET: Admin
        [HttpGet]
   
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [HttpGet]
        //[ValidateAntiForgeryToken]

        public ActionResult EmployeeList()
        {
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            List<Employeeinfo> EmployeeList = DbContext.Employeeinfoes.ToList();
            return View(EmployeeList);

        }

        [HttpGet]
      
        public ActionResult AddEmployee()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddEmployee(Employeeinfo MODEL)
        {
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            if (ModelState.IsValid)
            {
                DbContext.Employeeinfoes.Add(MODEL);
                DbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            return View();
        }
        [HttpGet]
   
        public ActionResult EditEmployee(int? id )
        {
            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();


            return View(employee);
        }
        [HttpPost]
     
        public ActionResult EditEmployee(Employeeinfo model,int id )
        {
            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employee != null)
            {
                employee.EmployeeName = model.EmployeeName;
                employee.Email = model.Email;
                employee.Contact = model.Contact;
                employee.Address = model.Address;
                employee.Department = model.Department;
                employee.Designation = model.Designation;
                employee.Userlogin.Username = model.Userlogin.Username;
                employee.Userlogin.Password = model.Userlogin.Password;
                employee.Userlogin.Role = model.Userlogin.Role;
            }
            if (ModelState.IsValid)
            {
                DbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            return View(employee);
        }
        [HttpDelete]
     
        public ActionResult DeleteEmployee(int? id)
        {

            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            DbContext.Employeeinfoes.Remove(employee);
            DbContext.SaveChanges();
            return RedirectToAction("EmployeeList");
        }


        [HttpGet]
  
        public ActionResult AttendanceReport()
        {
            var employees = DbContext.Employeeinfoes.Select(e => new { e.EmployeeId, e.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(e => new SelectListItem { Value = e.EmployeeId.ToString(), Text = e.EmployeeName }).ToList();
       
            return View(); // Pass an empty list initially
        }
        [HttpPost]
     
        public ActionResult AttendanceReport(int employeeId)
        {
            var employees = DbContext.Employeeinfoes.Select(e => new { e.EmployeeId, e.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(e => new SelectListItem { Value = e.EmployeeId.ToString(), Text = e.EmployeeName }).ToList();

            if (employeeId != 0)
            {
                var selectedEmployee = DbContext.Employeeinfoes.Find(employeeId);
                if (selectedEmployee != null)
                {
                    ViewBag.SelectedEmployeeName = selectedEmployee.EmployeeName;
              

                    var attendances = DbContext.Attendances
                    .Where(a => a.EmployeeId == employeeId)
                     .ToList();

                    return View(attendances);
                }
            }

           
            return View(new List<Attendance>());
        }

        [HttpGet]

        public ActionResult EditAttendance(int? id)
        {

            var attendance = DbContext.Attendances.Where(x => x.EmployeeId == id).FirstOrDefault();
            var employees = DbContext.Employeeinfoes.Select(e => new { e.EmployeeId, e.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(e => new SelectListItem { Value = e.EmployeeId.ToString(), Text = e.EmployeeName }).ToList();

            return View(attendance);
        }
        [HttpPost]
     
        public ActionResult EditAttendance(AttendanceViewModel model, int id)
        {
          
            var employees = DbContext.Employeeinfoes.Select(e => new { e.EmployeeId, e.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(e => new SelectListItem { Value = e.EmployeeId.ToString(), Text = e.EmployeeName }).ToList();
    
            if (ModelState.IsValid)
            {
                var attendance = DbContext.Attendances.Find(model.AttendanceId);
                if (attendance == null)
                {
                    return HttpNotFound();
                }

                attendance.Employeeinfo.EmployeeName = model.Employeeinfo.EmployeeName;
                attendance.AttendanceDate = model.AttendanceDate;
                attendance.Timestamp = model.Timestamp;
                attendance.EntryType = model.EntryType.ToString();
                DbContext.SaveChanges();
                return RedirectToAction("AttendanceReport");
            }

     
            return View(model);
        }
        [HttpGet]
      
        public ActionResult DeleteAttendance(int? id)
        {

            var attendance = DbContext.Attendances.Where(x => x.EmployeeId == id).FirstOrDefault();
            DbContext.Attendances.Remove(attendance);
            DbContext.SaveChanges();
            return RedirectToAction("AttendanceReport");
        }


    }

}




