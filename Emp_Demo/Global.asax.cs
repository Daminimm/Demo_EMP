using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Emp_Demo.App_Start;

namespace Emp_Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Optimization;
//using System.Web.Routing;
//using System.Web.Security;
//using Emp_Demo.App_Start;

//namespace Emp_Demo
//{
//    public class MvcApplication : System.Web.HttpApplication
//    {
//        protected void Application_Start()
//        {
//            AreaRegistration.RegisterAllAreas();
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
//        }

//        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
//        {
//            if (HttpContext.Current.User != null)
//            {
//                if (HttpContext.Current.User.Identity.IsAuthenticated)
//                {
//                    string username = HttpContext.Current.User.Identity.Name;
//                    var userRole = HttpContext.Current.Session["Role"]?.ToString();
//                    if (userRole != null)
//                    {
//                        string[] roles = new string[] { userRole };
//                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(HttpContext.Current.User.Identity, roles);
//                    }
//                }
//            }
//        }
//    }
//}

