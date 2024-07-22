using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Models;
using Emp_Demo.CustomValidation;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;

namespace Emp_Demo.Controllers
{
  
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Userlogin model)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Userlogins.FirstOrDefault(u => u.Username == model.Username);
                if (user != null)
                {
                    var hash = HashPassword(model.Password, user.PasswordSalt);
                    if (user.PasswordHash == hash)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, false);
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
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }
        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();


            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Logout")]
        public ActionResult LogoutPost()
        {
            FormsAuthentication.SignOut();
            Session.Clear();


            return RedirectToAction("Login");
        }
       
        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public ActionResult Register(Userlogin model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var salt = GenerateSalt();
                    var hash = HashPassword(model.Password, salt);
                    var user = new Userlogin
                    {

                        UserId = model.UserId,
                        Username = model.Username,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Role = model.Role,
                        Password = model.Password,

                    };
                    DbContext.Userlogins.Add(user);
                    DbContext.SaveChanges();

              
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception )
                {
                   
                    ModelState.AddModelError("", "An error occurred while creating the account. Please try again.");
                }

            }
            return View(model);
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
         
        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }


    }
}
