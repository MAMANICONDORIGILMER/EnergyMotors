using System.Web;
using System.Web.Optimization;

namespace EnergyMonitor
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/site.css"));

            // JS
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Content/js/jquery-{version}.js",
                        "~/Content/js/bootstrap.js"));

            // ⚙️ Permite minificar y agrupar en modo Release
            BundleTable.EnableOptimizations = true;
        }
    }
}
