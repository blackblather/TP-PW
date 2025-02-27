﻿using System.Web;
using System.Web.Optimization;

namespace TP_PW
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui.unobtrusive-{version}.js"));


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
                */

            /*CUSTOM STYLE BUNDLE*/
            bundles.Add(new StyleBundle("~/Content/templateStyles").Include("~/Content/owl.carousel.css",
                                                              "~/Content/barfiller.css",
                                                              "~/Content/animate.css",
                                                              "~/Content/font-awesome.min.css",
                                                              "~/Content/bootstrap.min.css",
                                                              "~/Content/slicknav.css",
                                                              "~/Content/magnific.popup.css",
                                                              "~/Content/main.css"));
            bundles.Add(new StyleBundle("~/Content/datepickerStyles").Include("~/Content/datepicker/datepicker.min.css"));

            /*CUSTOM SCRIPT BUNDLE*/
            bundles.Add(new ScriptBundle("~/Scripts/templateScripts").Include("~/Scripts/jquery-2.2.4.min.js",
                                                                              "~/Scripts/vendor/popper.min.js",
                                                                              "~/Scripts/vendor/bootstrap.min.js",
                                                                              "~/Scripts/vendor/owl.carousel.min.js",
                                                                              "~/Scripts/vendor/isotope.pkgd.min.js",
                                                                              "~/Scripts/vendor/jquery.barfiller.js",
                                                                              "~/Scripts/vendor/loopcounter.js",
                                                                              "~/Scripts/vendor/slicknav.min.js",
                                                                              "~/Scripts/active.js"));

            bundles.Add(new ScriptBundle("~/Scripts/datepickerScripts").Include("~/Scripts/datepicker/datepicker.min.js",
                                                                                "~/Scripts/datepicker/datepicker.pt-BR.js"));
            

            bundles.Add(new ScriptBundle("~/Scripts/maskJs").Include("~/Scripts/jquery.mask.js"));

        }
    }
}
