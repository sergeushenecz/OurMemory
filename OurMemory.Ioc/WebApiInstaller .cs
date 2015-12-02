using System.Reflection;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OurMemory.Service;
using OurMemory.Service.Interfaces;

namespace OurMemory
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleScoped()
                );

            container.Register(Component.For<IVeteranService>().ImplementedBy<VeteranService>().LifestyleTransient());
        }
    }
}