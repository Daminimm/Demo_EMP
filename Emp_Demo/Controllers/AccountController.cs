using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Models;
using Emp_Demo.CustomValidation;

namespace Emp_Demo.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
    
        public ActionResult Login()
        {
            return View();
        }
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
        [HttpGet]
        public ActionResult Logout()
        {
            
            Session.Clear();

            
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogoutPost()
        {
            
            Session.Clear();

        
            return RedirectToAction("Login");
        }
    }
}
