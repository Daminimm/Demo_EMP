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
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            List<Employeeinfo> EmployeeList = DbContext.Employeeinfoes.ToList();
            return View(EmployeeList);
        
        }
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
                return RedirectToAction("AdminDashboard");
            }

            return View();
        }

        
    }
}