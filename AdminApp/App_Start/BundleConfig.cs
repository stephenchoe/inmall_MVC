﻿using System.Web;
using System.Web.Optimization;

namespace AdminApp
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                   "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
              "~/Scripts/jquery.loading.js",
               "~/Scripts/sweet-alert.js",
                    "~/Scripts/site.js"));


          

            bundles.Add(new StyleBundle("~/Content/css").Include(                     
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/all.css",
                    "~/Content/themes/base/core.css",
                    "~/Content/themes/base/resizable.css",
                    "~/Content/themes/base/selectable.css",
                    "~/Content/themes/base/accordion.css",
                    "~/Content/themes/base/button.css",
                    "~/Content/themes/base/dialog.css",
                    "~/Content/themes/base/slider.css",
                    "~/Content/themes/base/tabs.css",
                    "~/Content/themes/base/datepicker.css",
                    "~/Content/themes/base/progressbar.css",
                    "~/Content/themes/base/theme.css",
                    "~/Content/themes/base/autocomplete.css"));
        }
    }
}
