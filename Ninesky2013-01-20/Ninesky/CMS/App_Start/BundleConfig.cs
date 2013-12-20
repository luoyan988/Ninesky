using System.Web;
using System.Web.Optimization;

namespace Ninesky
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryUi").Include(
                        "~/Scripts/jquery-ui-{version}.js", "~/Scripts/jquery.ui.datepicker-zh-CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/EasyUi").Include(
                        "~/Scripts/EasyUi/easyloader.js"));

            bundles.Add(new ScriptBundle("~/bundles/kindeditor").Include(
                        "~/Scripts/kindeditor/kindeditor-min.js", "~/Scripts/kindeditor/lang/zh_CN.js"));

            bundles.Add(new ScriptBundle("~/Ztree").Include(
                        "~/Scripts/Ztree/jquery.ztree.core-{version}.js"));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Css").Include("~/Content/Default/Style.css"));
            bundles.Add(new StyleBundle("~/UserCss").Include("~/Content/Default/User/Style.css"));
            bundles.Add(new StyleBundle("~/ManageCss").Include("~/Content/Default/Manage/Style.css"));
            bundles.Add(new StyleBundle("~/EasyUi/icon").Include("~/Scripts/EasyUi/themes/icon.css"));
            bundles.Add(new StyleBundle("~/JqueryUiMetro").Include("~/Content/JqueryUi/Metro/jquery-ui-{version}.css"));
            bundles.Add(new StyleBundle("~/ZtreeCss").Include("~/Content/zTree/Default.css"));
        }
    }
}