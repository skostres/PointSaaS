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
                      "~/Content/bootstrap.css",
                      "~/Content/ui-bootstrap-csp.css"));

            bundles.Add(new StyleBundle("~/Content/sb-admin").Include(
                      "~/Content/sb-admin.css",
                       "~/Content/plugins/morris.css",
                       "~/Content/plugins/ng-alerts.min.css",
                       "~/Content/plugins/ng-table.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/Site.css",
                      "~/Content/plugins/epoch.min.css",
                      "~/Content/plugins/angular-growl.min.css"));

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
                     "~/scripts/angular-ui-router.js",

                     // UI Bootstrap
                     "~/scripts/angular-ui/ui-bootstrap-tpls.min.js",
                     "~/scripts/angular-ui/ui-bootstrap.min.js",

                     // NG-Table
                     "~/scripts/plugins/ng-table/ng-table.js",

                     // EPOCH Module
                     "~/scripts/d3/d3.min.js",
                     "~/scripts/plugins/epoch/epoch.min.js",
                     "~/scripts/plugins/epoch/ng-epoch.js",

                     // N3 pie chart
                     "~/scripts/plugins/n3-pie-chart/pie-chart.min.js",

                     "~/scripts/plugins/modal-service/angular-modal-service.min.js",
                     // Ng-file-model: https://github.com/mistralworks/ng-file-model/
                     "~/scripts/plugins/ng-file-model/ng-file-model.js",

                     "~/scripts/plugins/angular-growl/angular-growl.min.js",

                     "~/scripts/angular-signalr-hub.min.js",
                     "~/Client/Common/POCO/Level.js",
                     "~/Client/Common/POCO/Team.js",
                      "~/Client/Common/POCO/TeamSubmission.js",
                     "~/Client/Common/POCO/MsgType.js",
                     "~/Client/Common/POCO/Role.js",
                     "~/Client/Common/POCO/User.js",
                     "~/Client/Common/POCO/Project.js",
                     "~/Client/Service/Session.js",
                     "~/Client/Service/AuthService.js",
                     
                    "~/Client/Common/BaseCtrl.js",
                    "~/Client/Common/Notification.js",

                    // Modals
                    "~/Client/Common/GenericYesNoModalCtrl.js",


                    
                   "~/Client/Hubs/Communication.js",
                    //"~/scripts/angular-route.min.js",
                    "~/Client/Navigation/NavigationCtrl.js",
                    "~/Client/Pages/GradeSubmissions/GradeSubmissionsCtrl.js",
                    "~/Client/Pages/SubmitProjects/SubmitProjectsCtrl.js",
                    "~/Client/Pages/ManageProjects/ManageProjects.js",
                    "~/Client/Notifications/NotificationsCtrl.js",
                     "~/Client/Pages/Login/LoginCtrl.js",
                    "~/Client/Pages/Dashboard/DashboardCtrl.js",
                    "~/Client/Pages/Projects/ProjectsCtrl.js",
                    "~/Client/Service/SystemSrv.js",
                    "~/Client/App.js"));
        }
    }
}