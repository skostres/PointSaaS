using System.Web;
using System.Web.Optimization;

namespace PCMC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.0.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            */
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/sb-admin").Include(
                      "~/Content/sb-admin.css",
                       "~/Content/plugins/morris.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/plugins/flot").Include(
                    "~/Scripts/plugins/flot/jquery.flot.js",
                    "~/Scripts/plugins/flot/jquery.flot-min.js",
                    "~/Scripts/plugins/flot/jquery.flot.pie.js",
                    "~/Scripts/plugins/flot/jquery.flot.resize.js",
                    "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                    "~/Scripts/plugins/flot/excanvas.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins/morris").Include(
                    "~/Scripts/plugins/morris/morris.js",
                    "~/Scripts/plugins/morris/morris-min.js",
                    "~/Scripts/plugins/morris/raphael.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/scripts/angular.js",
                    "~/scripts/angular-animate.min.js",
                    "~/Client/Common/BaseCtrl.js",
                    "~/Client/Common/Notification.js",
                    "~/scripts/angular-route.min.js",
                    "~/Client/Navigation/NavigationCtrl.js",
                    "~/Client/Notifications/NotificationsCtrl.js",
                    "~/Client/Pages/Dashboard/DashboardCtrl.js",
                    "~/Client/Pages/Projects/ProjectsCtrl.js",
                    "~/Client/Service/SystemSrv.js",
                    "~/Client/App.js"));
        }
    }
}