using System.Web;
using System.Web.Optimization;

namespace WebApp
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

           

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                 "~/Scripts/jquery.loading.js",
                  "~/Scripts/sweet-alert.js",
                       "~/Scripts/site.js"));


            //Style
            bundles.Add(new StyleBundle("~/Content/css/css").Include("~/Content/css/*.css"));


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
