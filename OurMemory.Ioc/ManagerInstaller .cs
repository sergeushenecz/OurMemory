using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OurMemory.Ioc
{
    public class ManagerInstaller 
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Where(type => type.Name.EndsWith("Manager"))
                .WithServiceDefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));
        } 
    }
}