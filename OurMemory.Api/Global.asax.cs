using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;


namespace OurMemory
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;



        protected void Application_Start()
        {
            AutoMapperConfig mapperConfig = new AutoMapperConfig();
            mapperConfig.Initialization();

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
