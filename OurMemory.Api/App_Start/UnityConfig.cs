using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OurMemory.Controllers;
using OurMemory.Data;
using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;
using OurMemory.Service;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Services;
using OurMemory.Service.Specification;
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
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IImageVeteranService, ImageVeteranService>(new HierarchicalLifetimeManager());
            container.RegisterType<IArticleService, ArticleService>(new HierarchicalLifetimeManager());

            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType(typeof(IFormService<>), typeof(FormService<>));


            container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(t => t.BaseType == typeof(SpecificationBase<>)), WithMappings.FromMatchingInterface, WithName.Default);

            // TODO: Register your types here
            container.RegisterType<IUserStore<User>, UserStore<User>>();
            container.RegisterType<UserManager<User>>();
            container.RegisterType<User>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor(typeof(ApplicationUserManager)));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}