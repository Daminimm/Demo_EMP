using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class BaseControllerController : Controller
    {
        // GET: BaseController
        protected int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }
            throw new Exception("User ID not found in session.");
        }
    }
}