using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emp_Demo.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }
            throw new Exception("User ID not found in session.");

        }
        protected int GetLoggedInEmployeeId()
        {
            if (Session["EmployeeId"] != null)
            {
                return (int)Session["EmployeeId"];
            }
            throw new Exception("User ID not found in session.");
        }
    }
}