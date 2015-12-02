using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Practices.Unity;
using OurMemory.Ioc;
using OurMemory.Service;
using OurMemory.Service.Interfaces;


namespace OurMemory
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;
        
        
        
        protected void Application_Start()
        {
//            var container = new WindsorContainer();
//
//            container.Install(new WebApiInstaller());

            AutoMapperConfig mapperConfig = new AutoMapperConfig();

            mapperConfig.Initialization();

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents(); 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
//            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(container.Kernel);

          
        }
    }
}
