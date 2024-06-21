using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {

            return View();
        }
        [HttpGet]
        public ActionResult EmployeeManagement()
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
                return RedirectToAction("EmployeeManagement");
            }

            return View();
        }
        [HttpGet]
        public ActionResult AttendanceManagement()
        {

            return View();
        }
        [HttpGet]
        public ActionResult AttendanceReport()
        {
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            var employeeNames = DbContext.Employeeinfoes.Select(x => x.EmployeeName).ToList();
            ViewBag.employeeList = employeeNames.Select(name => new SelectListItem { Value = name, Text = name }).ToList();

            return View();
        }

  
        [HttpPost]
        public ActionResult AttendanceReport(Employeeinfo MODEL)
        {


            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            var employeeNames = DbContext.Employeeinfoes.Select(x => x.EmployeeName).ToList();
            ViewBag.employeeList = employeeNames.Select(name => new SelectListItem { Value = name, Text = name }).ToList();

            return View();
        }

        //public ActionResult AttendanceReport()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AttendanceReport(Employeeinfo MODEL)
        //{
        //    Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        //    List<Employeeinfo> employeeList = DbContext.Employeeinfoes.ToList();
        //    ViewBag.UserList = employeeList.Select(x => new SelectListItem { Value = x.EmployeeId.ToString(), Text = x.EmployeeName }).ToList();

        //    //var employees = DbContext.Employeeinfoes.ToList();

        //    //var employeeList = new SelectList( "EmployeeId");

        //    //ViewBag.EmployeeList = employeeList; 

        //    return View();
        //}


    }
}