using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web;

namespace firewood
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SiteConfig.SiteUrl = "http://ghy.cn/firewood"; // "http://localhost:2816";
            SiteConfig.SitePath = HttpContext.Current.Server.MapPath("~");

            //设置MEF依赖注入容器
            DirectoryCatalog catalog = new DirectoryCatalog(HttpContext.Current.Server.MapPath("~/EFAssemblies/"));
            SiteConfig.Container = new CompositionContainer(catalog);
            SiteConfig.Container.ComposeParts(this);
        }
    }
}
