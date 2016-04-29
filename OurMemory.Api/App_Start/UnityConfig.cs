using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using OurMemory.Controllers;
using OurMemory.Data;
using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;
using OurMemory.Hubs;
using OurMemory.Service;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Services;
using OurMemory.Service.Specification;
using OurMemory.UnityResolvers;
using Unity.WebApi;
using Article = OurMemory.Service.Services.Article;

namespace OurMemory
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IVeteranService, VeteranService>();
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IImageVeteranService, ImageVeteranService>(new HierarchicalLifetimeManager());
            container.RegisterType<IArticle, Article>();
            container.RegisterType<IComment, Article>("ArticleServiceComment");
            container.RegisterType<ICommentService, CommentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhotoAlbumService, PhotoAlbumService>(new HierarchicalLifetimeManager());

            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType(typeof(IFormService<>), typeof(FormService<>));


            container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(t => t.BaseType == typeof(SpecificationBase<>)),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            // TODO: Register your types here
            container.RegisterType<IUserStore<User>, UserStore<User>>();
            container.RegisterType<UserManager<User>>();
            container.RegisterType<User>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor(typeof(ApplicationUserManager),typeof(IUserService)));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            GlobalHost.DependencyResolver = new SignalRUnityDependencyResolver(container);

            container.RegisterType<CommentHub>(new InjectionFactory(CreateCommentHub));

        }

        private static object CreateCommentHub(IUnityContainer p)
        {
            var myHub = new CommentHub(p, p.Resolve<ICommentService>());

            return myHub;
        }
    }
}