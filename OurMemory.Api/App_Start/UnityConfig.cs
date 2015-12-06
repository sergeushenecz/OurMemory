using Microsoft.Practices.Unity;
using System.Web.Http;
using OurMemory.Data.Infrastructure;
using OurMemory.Service;
using OurMemory.Service.Interfaces;
using Unity.WebApi;

namespace OurMemory
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IVeteranService, VeteranService>(new HierarchicalLifetimeManager());

            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType(typeof(IFormService<>), typeof(FormService<>));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}