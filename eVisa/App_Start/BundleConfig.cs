using System.Web;
using System.Web.Optimization;

namespace eVisa
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.10.2.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-ui-router.min.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-alertify.js",
                "~/App/main.js"
            ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-js").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/materialize.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/font-awesome.min.css",
                "~/Content/material_icon.css"
             ));
            
            bundles.Add(new StyleBundle("~/Content/bootstrap-css").Include(
                //"~/Content/bootstrap.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/bootstrap_style_lib/theme-default.css"
             ));

            bundles.Add(new StyleBundle("~/Content/materialize-css").Include(
                "~/Content/materialize.css",
                "~/Content/angular-material.min.css"
             ));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/master_main.css",
                "~/Content/style.css"
            ));

            bundles.Add(new StyleBundle("~/Content/dropdown").Include(
                 "~/Content/dropdowns-enhancement.css"
            ));

            bundles.Add(new StyleBundle("~/Content/SlideImage").Include(
                "~/Content/SlideImage/SlideShow.css"
            ));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                 "~/Content/bootstrap_style_lib/theme-default.css"
            ));

            //custom admin
            bundles.Add(new ScriptBundle("~/bundles/lib-js").Include(
                "~/Scripts/dropdowns-enhancement.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/tinymce/tinymce.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/bootstrap/plugins/jquery/jquery-ui.min.js",
                "~/Scripts/bootstrap/plugins/icheck/icheck.min.js",
                "~/Scripts/bootstrap/plugins/mcustomscrollbar/jquery.mCustomScrollbar.min.js",
                "~/Scripts/bootstrap/plugins/moment.min.js",
                "~/Scripts/bootstrap/plugins/fullcalendar/fullcalendar.min.js",
                "~/Scripts/bootstrap/plugins.js",
                "~/Scripts/bootstrap/actions.js"
            ));

        }
    }
}
