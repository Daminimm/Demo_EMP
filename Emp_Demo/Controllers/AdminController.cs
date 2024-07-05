using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        public ActionResult AddEmployee(EmployeeinfoViewModel model)
        {
            try
            {
                Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
                {
                    if (ModelState.IsValid)
                    {
                     
                        var employee = new Employeeinfo
                        {
                            EmployeeId=model.EmployeeId,
                            EmployeeName = model.EmployeeName,
                            Email = model.Email,
                            Contact = model.Contact,
                            Department = model.Department,
                            Address = model.Address,
                            Designation = model.Designation,
                            UserId = model.UserId,
                            Userlogin = new Userlogin
                            {
                                Username = model.Username,
                                Password = model.Password,
                                Role = model.Role
                            }

                        };

                        DbContext.Employeeinfoes.Add(employee);
                        DbContext.SaveChanges();
                        return RedirectToAction("EmployeeList");
                    }
                }
            }
          
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
       

            return View(model);
        }







        //[HttpGet]

        //public ActionResult AddEmployee()
        //{
        //    return View();


        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddEmployee(Employeeinfo model)
        //{
        //    try
        //    {
        //        using (var DbContext = new Demo_EmployeeManagementEntities())
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                DbContext.Employeeinfoes.Add(model);
        //                DbContext.SaveChanges();
        //                return RedirectToAction("EmployeeList");
        //            }
        //        }
        //    }
        //    catch (Exception )
        //    {
        //        ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
        //    }


        //    return View(model);
        //}


        //[HttpPost]
        ////[ValidateAntiForgeryToken]

        //public ActionResult AddEmployee(Employeeinfo MODEL)
        //{


        //    Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();

        //    if (ModelState.IsValid)
        //    {
        //        DbContext.Employeeinfoes.Add(MODEL);
        //        DbContext.SaveChanges();
        //        return RedirectToAction("EmployeeList");
        //    }


        //    return View();
        //}
        [HttpGet]

        public ActionResult EditEmployee(int id)
        {
            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]

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
                DbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            return View(employee);
        }
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
            var employees = DbContext.Employeeinfoes.Select(x => new { x.EmployeeId, x.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(x => new SelectListItem { Value = x.EmployeeId.ToString(), Text = x.EmployeeName }).ToList();
            return View();
        }


        [HttpPost]

        public ActionResult AttendanceReport(int? employeeId)
        {
            var employees = DbContext.Employeeinfoes.Select(X => new { X.EmployeeId, X.EmployeeName }).ToList();
            ViewBag.employeeList = employees.Select(X => new SelectListItem { Value = X.EmployeeId.ToString(), Text = X.EmployeeName }).ToList();

            if (employeeId.HasValue)
            {
                var selectedEmployee = DbContext.Employeeinfoes.Find(employeeId);
                if (selectedEmployee != null)
                {
                    ViewBag.SelectedEmployeeName = selectedEmployee.EmployeeName;
                    var attendances = DbContext.Attendances
                    .Where(a => a.EmployeeId == employeeId)
                     .ToList();

                    return View("AttendanceReport", attendances);
                }
            }

            return View();
        }
        [HttpGet]
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

                    DbContext.SaveChanges();

                
                    return RedirectToAction("AttendanceReport", new { employeeId = model.EmployeeId });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }


            return View(model);
        }
        [HttpGet]

        public ActionResult DeleteAttendance(int? Id)
        {

            var attendance = DbContext.Attendances.Where(x => x.AttendanceId == Id).FirstOrDefault();
            DbContext.Attendances.Remove(attendance);
            DbContext.SaveChanges();
            return RedirectToAction("AttendanceReport");
        }


    }
  




}







