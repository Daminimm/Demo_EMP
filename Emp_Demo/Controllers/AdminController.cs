using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Emp_Demo.Models;

namespace Emp_Demo.Controllers
{
 
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {

        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        // GET: Admin
        [HttpGet]
        [Route("AdminDashboard")]
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [HttpGet]
        [Route("EmployeeList")]
        public ActionResult EmployeeList()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            List<Employeeinfo> EmployeeList = DbContext.Employeeinfoes.ToList();
            return View(EmployeeList);

        }

        [HttpGet]
        [Route("AddEmployee")]
        public ActionResult AddEmployee()
        {
            return View();


        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AddEmployee")]
        public ActionResult AddEmployee(EmployeeinfoViewModel model)
        {
            try
            {
                using (var DbContext = new Demo_EmployeeManagementEntities())
                {


                    if (ModelState.IsValid)
                    {
                        Userlogin userLogin = new Userlogin();
                        userLogin.Username = model.Username;
                        userLogin.Password = model.Password;
                        userLogin.Role = model.Role;
                        DbContext.Userlogins.Add(userLogin);
                        DbContext.SaveChanges();

                        model.UserId = userLogin.UserId;

                        Employeeinfo employee = new Employeeinfo
                        {
                            EmployeeName = model.EmployeeName,
                            Email = model.Email,
                            Contact = model.Contact,
                            Department = model.Department,
                            Address = model.Address,
                            Designation = model.Designation,
                            UserId = model.UserId
                        };

                        DbContext.Employeeinfoes.Add(employee);
                        TempData["message"] = "Employee Add Successfully";
                        DbContext.SaveChanges();
                      return RedirectToAction("EmployeeList");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }


            return View();
        }


        [HttpGet]
        [Route("EditEmployee/{id}")]
        public ActionResult EditEmployee(int id)
        {
            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]
        [Route("EditEmployee/{id}")]
        public ActionResult EditEmployee(Employeeinfo model, int id)
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
                TempData["message"] = "Employee Updated Successfully";
                DbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            return View(employee);
        }
        [HttpGet]
        [Route("DeleteEmployee/{id}")]
        public ActionResult DeleteEmployee(int? id)
        {

            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            DbContext.Employeeinfoes.Remove(employee);
            DbContext.SaveChanges();
            TempData["message"] = "Employee Deleted Successfully";
            return RedirectToAction("EmployeeList");
        }
      
        [HttpGet]
        [Route("AttendanceReport")]
        public ActionResult AttendanceReport(int? employeeId)
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }
            var employees = DbContext.Employeeinfoes.Select(x => new { x.EmployeeId, x.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(x => new SelectListItem { Value = x.EmployeeId.ToString(), Text = x.EmployeeName }).ToList();

            if (employeeId.HasValue)
            {
                var selectedEmployee = DbContext.Employeeinfoes.Find(employeeId);
                if (selectedEmployee != null)
                {
                    ViewBag.SelectedEmployeeName = selectedEmployee.EmployeeName;
                    var attendances = DbContext.Attendances
                        .Where(a => a.EmployeeId == employeeId)
                        .OrderBy(a => a.AttendanceDate)
                        .ToList();

                    return View(attendances);
                }
            }

            return View();
        }
        [HttpPost]
        [Route("AttendanceReport")]
        public ActionResult AttendanceReport(int employeeId)
        {
            return RedirectToAction("AttendanceReport", new { employeeId = employeeId });
        }

        [HttpGet]
        [Route("EditAttendance/{id}")]
        public ActionResult EditAttendance(int? id)
        {

            var attendance = DbContext.Attendances.Where(x => x.AttendanceId == id).FirstOrDefault();
        
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditAttendance/{id}")]
        public ActionResult EditAttendance(Attendance model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var attendance = DbContext.Attendances.Find(model.AttendanceId);
       

                    if (attendance == null)
                    {
                        return HttpNotFound();
                    }


                    attendance.EmployeeId = model.EmployeeId;
                    attendance.AttendanceDate = model.AttendanceDate;
                    attendance.Timestamp = model.Timestamp;
                    attendance.EntryType = model.EntryType;
                    TempData["message"] = "Attendance  Updated Successfully";
                    DbContext.SaveChanges();

                    return Json(new { success = true, employeeId = model.EmployeeId });
                    //return RedirectToAction("AttendanceReport", new { employeeId = model.EmployeeId });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }


            return View(model);
        }
        [HttpGet]
        [Route("DeleteAttendance/{id}")]
        public ActionResult DeleteAttendance(int? Id)
        {

            var attendance = DbContext.Attendances.Where(x => x.AttendanceId == Id).FirstOrDefault();
            DbContext.Attendances.Remove(attendance);
            TempData["message"] = "Attendance  Deleted Successfully";
            DbContext.SaveChanges();

            return RedirectToAction("AttendanceReport");
        }


    }
  
}







