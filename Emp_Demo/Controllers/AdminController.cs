﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Emp_Demo.Models;
using Newtonsoft.Json;

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
        public ActionResult EmployeeList( )
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
            var departments = new List<SelectListItem>
    {
        new SelectListItem { Value = "Technical", Text = "Technical" },
        new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }
    };

            ViewBag.Departments = new SelectList(departments, "Value", "Text");

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
                    var departments = new List<SelectListItem>
            {
                new SelectListItem { Value = "Technical", Text = "Technical" },
                new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }
            };
                    ViewBag.Departments = new SelectList(departments, "Value", "Text");

                   
                    if (DbContext.Userlogins.Any(u => u.Username == model.Username))
                    {
                        ModelState.AddModelError("Username", "Username already exists. Please choose a different one.");
                    }

                 
                    if (DbContext.Employeeinfoes.Any(e => e.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "Email address is already registered.");
                    }

                    if (ModelState.IsValid)
                    {
                   
                        Userlogin userLogin = new Userlogin
                        {
                            Username = model.Username,
                            Password = model.Password,
                            Role = model.Role
                        };
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
                        DbContext.SaveChanges();

                        TempData["message"] = "Employee added successfully.";
                        return RedirectToAction("EmployeeList");
                    }
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }

            return View(model);
        }

        //[HttpGet]

        //[Route("AddEmployee")]
        //public ActionResult AddEmployee()
        //{
        //    var departments = new List<SelectListItem>
        //{
        //    new SelectListItem { Value = "Technical", Text = "Technical" },
        //    new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }

        //};

        //    ViewBag.Departments = new SelectList(departments, "Value", "Text");

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("AddEmployee")]
        //public ActionResult AddEmployee(EmployeeinfoViewModel model)
        //{
        //    try
        //    {
        //        using (var DbContext = new Demo_EmployeeManagementEntities())
        //        {
        //            var departments = new List<SelectListItem>
        //            {
        //              new SelectListItem { Value = "Technical", Text = "Technical" },
        //              new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }

        //            };
        //            ViewBag.Departments = new SelectList(departments, "Value", "Text");


        //            if (ModelState.IsValid)
        //            {
        //                Userlogin userLogin = new Userlogin();
        //                userLogin.Username = model.Username;
        //                userLogin.Password = model.Password;
        //                userLogin.Role = model.Role;
        //                DbContext.Userlogins.Add(userLogin);
        //                DbContext.SaveChanges();

        //                model.UserId = userLogin.UserId;

        //                Employeeinfo employee = new Employeeinfo
        //                {
        //                    EmployeeName = model.EmployeeName,
        //                    Email = model.Email,
        //                    Contact = model.Contact,
        //                    Department = model.Department,
        //                    Address = model.Address,
        //                    Designation = model.Designation,
        //                    UserId = model.UserId
        //                };

        //                DbContext.Employeeinfoes.Add(employee);
        //                TempData["message"] = "Employee Add Successfully";
        //                DbContext.SaveChanges();
        //              return RedirectToAction("EmployeeList");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
        //    }


        //    return View();
        //}


        [HttpGet]
        [Route("EditEmployee/{id}")]
        public ActionResult EditEmployee(int id)
        {
             var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
             var departments = new List<SelectListItem>
             {
               new SelectListItem { Value = "Technical", Text = "Technical" },
              new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }
         
             };
            ViewBag.Departments = new SelectList(departments, "Value", "Text", employee.Department);
            return View(employee);
        }
        [HttpPost]
        [Route("EditEmployee/{id}")]
        public ActionResult EditEmployee(Employeeinfo model, int id)
        {
            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            var departments = new List<SelectListItem>
            {
               new SelectListItem { Value = "Technical", Text = "Technical" },
               new SelectListItem { Value = "Non-Technical", Text = "Non-Technical" }

            };
            ViewBag.Departments = new SelectList(departments, "Value", "Text", employee.Department);
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
        [Route("DeleteEmployee")]
        public ActionResult DeleteEmployee(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }

            
            //var attendances = DbContext.Attendances.Where(a => a.EmployeeId == id).ToList();

        
            //foreach (var attendance in attendances)
            //{
            //    DbContext.Attendances.Remove(attendance);
            //}
            DbContext.Employeeinfoes.Remove(employee);
            DbContext.SaveChanges();
            TempData["message"] = "Employee Deleted Successfully";
            return RedirectToAction("EmployeeList");
        }
        //[HttpGet]
        //[Route("DeleteEmployee")]
        //public ActionResult DeleteEmployee(int? id)
        //{

        //    var employee = DbContext.Employeeinfoes.Where(x => x.EmployeeId == id).FirstOrDefault();
        //    DbContext.Employeeinfoes.Remove(employee);
        //    DbContext.SaveChanges();
        //    TempData["message"] = "Employee Deleted Successfully";
        //    return RedirectToAction("EmployeeList");
        //}
      
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
                  
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }


            return View("attendance");
        }
        [HttpGet]
       
        [Route("DeleteAttendance/{id}")]
        public ActionResult DeleteAttendance(int id)
        {
            try
            {
                var attendance = DbContext.Attendances.Find(id);
                if (attendance == null)
                {
                    return HttpNotFound();
                }

                DbContext.Attendances.Remove(attendance);
                TempData["message"] = "Attendance Deleted Successfully";
                DbContext.SaveChanges();
            
                return Json(new { success = true, employeeId = attendance.EmployeeId },JsonRequestBehavior.AllowGet);
              
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

            return View();
        }

       


    }
  
}







