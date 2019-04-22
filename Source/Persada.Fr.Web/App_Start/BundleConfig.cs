using System.Web;
using System.Web.Optimization;

namespace Persada.Fr.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/assets/css/font-awesome.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Persada/css").Include(
                     "~/Content/Persada/css/custom-persada.css",
                      "~/Content/Persada/css/bootstrap-social.css"));

            bundles.Add(new ScriptBundle("~/Persada/js").Include(
                     "~/Content/Persada/js/Helpers/Modal.js",
                     "~/Content/Persada/js/Helpers/Common.js"
                     ));

            bundles.Add(new StyleBundle("~/Gemagrafi/css").Include(
                  "~/Content/Gemagrafi/css/style.css"));

            bundles.Add(new StyleBundle("~/Metronic/css").Include(
                   "~/Content/Metronics/global/plugins/font-awesome/css/font-awesome.min.css",
                   "~/Content/Metronics/global/plugins/simple-line-icons/simple-line-icons.min.css",
                   "~/Content/Metronics/global/plugins/bootstrap/css/bootstrap.min.css",
                   "~/Content/Metronics/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                   "~/Content/Metronics/global/plugins/gritter/css/jquery.gritter.css",
                   "~/Content/Metronics/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                   "~/Content/Metronics/global/plugins/fullcalendar/fullcalendar.min.css",
                   "~/Content/Metronics/global/plugins/pages/css/tasks.css",
                   "~/Content/Metronics/global/plugins/css/components.css",
                   "~/Content/Metronics/global/plugins/css/plugins.css",
                   "~/Content/Metronics/global/plugins/layout/css/layout.css",
                   "~/Content/Metronics/global/plugins/layout/css/themes/default.css",
                   "~/Content/Metronics/global/plugins/layout/css/custom.css",
                   "~/Content/Metronics/global/plugins/bootstrap-datepicker/css/datepicker3.css",
                   "~/Content/Metronics/global/plugins/bootstrap-datepicker/css/datepicker.css",
                   "~/Content/Metronics/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css"
                   ));

                bundles.Add(new ScriptBundle("~/Metronic/js").Include(
                "~/Content/Metronics/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/Metronics/global/plugins/jquery.blockui.min.js",
                 "~/Content/Metronics/global/plugins/scripts/jquery.notific8.min.js",
                 "~/Content/Metronics/global/plugins/scripts/toastr.min.js",
                "~/Content/Metronics/global/plugins/scripts/app.min.js",
                "~/Content/Metronics/global/plugins/scripts/moment.min.js",
                "~/Content/Metronics/global/plugins/fullcalendar/fullcalendar.min.js",
                "~/Content/Metronics/global/plugins/jquery-ui/jquery-ui.min.js",                 
                "~/Content/Metronics/global/plugins/fullcalendar/calendar.min.js",
                "~/Content/Metronics/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/Metronics/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/Metronics/global/plugins/flot/jquery.flot.min.js",
                "~/Content/Metronics/global/plugins/flot/jquery.flot.resize.min.js",
                "~/Content/Metronics/global/plugins/flot/jquery.flot.categories.min.js",
                //"~/Content/Metronics/global/plugins/jquery.pulsate.min.js",
                "~/Content/Metronics/global/plugins/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/Metronics/global/plugins/gritter/js/jquery.gritter.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Content/Metronics/global/plugins/scripts/metronic.js",
                "~/Content/Metronics/global/plugins/layout/scripts/layout.js",
                "~/Content/Metronics/global/plugins/layout/scripts/quick-sidebar.js",
                "~/Content/Metronics/global/plugins/pages/scripts/index.js",
                "~/Content/Metronics/global/plugins/pages/scripts/tasks.js",
                //"~/Content/Metronics/global/plugins/jquery.bootpag.min.js",
                "~/Content/Metronics/global/plugins/holder.js",
                "~/Content/Metronics/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js"
               ));
        }
    }
}

