using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace firewood
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //shared

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Resources/Bootstrap/css/bootstrap.min.css",
                "~/Resources/Bootstrap/css/bootstrap-datepicker.min.css",
                "~/Resources/Site/css/common.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Resources/JQuery/jquery-1.11.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Resources/JQuery/jquery.request.js",
                "~/Resources/Bootstrap/js/bootstrap.js",
                "~/Resources/Bootstrap/js/bootstrap-datepicker.min.js",
                "~/Resources/Bootstrap/js/datepicker.config.js",
                "~/Resources/Site/js/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie8").Include(
                "~/Resources/Bootstrap/js/modernizr-2.6.2.js",
                "~/Resources/Bootstrap/js/respond.js"));
            //index
            bundles.Add(new StyleBundle("~/bundles/index_styles").Include(
                "~/Resources/Site/css/index.css"));
            //index js
            bundles.Add(new ScriptBundle("~/bundles/index_scripts").Include(
                "~/Resources/Site/js/index.js"));
            //form
            bundles.Add(new StyleBundle("~/bundles/form_styles").Include(
                "~/Resources/Site/css/form.css"));
            //publish js
            bundles.Add(new ScriptBundle("~/bundles/publish_scripts").Include(
                "~/Resources/Site/js/publish.js"));
            //org
            bundles.Add(new StyleBundle("~/bundles/org_styles").Include(
                "~/Resources/Site/css/org.css"));
            //page js
            bundles.Add(new ScriptBundle("~/bundles/page_scripts").Include(
                "~/Resources/Site/js/page.js"));
        }
    }
}