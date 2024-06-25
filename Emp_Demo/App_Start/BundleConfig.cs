using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Emp_Demo.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-2.0.0.js",
                       "~/Scripts/jquery-2.0.0.min.js",
                       "~/Scripts/jquery-ui-1.13.2.js",
                       "~/Scripts/jquery-ui-1.13.2.min.js"));

            // Bootstrap bundle
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap.bundle.js",
                        "~/Scripts/bootstrap.bundle.min.js",
                        "~/Scripts/bootstrap.esm.js",
                        "~/Scripts/bootstrap.esm.min.js"));

            // CSS bundle (if needed)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"));

            // Enable bundling and minification
            BundleTable.EnableOptimizations = true;
        }

    }
}