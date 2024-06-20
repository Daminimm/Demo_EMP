using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Userlogins.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
              
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else if (user.Role == "Employee")
                    {
                        return RedirectToAction("EmployeeDashboard", "Employee");
                    }
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }
    }
}
