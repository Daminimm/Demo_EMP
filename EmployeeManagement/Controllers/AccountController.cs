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
      
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Userlogin MODEL)
        {
            Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
            if (ModelState.IsValid)
            {
                var user = DbContext.Userlogins.FirstOrDefault(u => u.Username == MODEL.Username && u.Password == MODEL.Password);

                if (user != null)
                {
                    Session["UserId"] = user.UserId;  

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

            return View(MODEL);
        }
    }
}
