using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OurMemory;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace OurMemory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration configuration = new HttpConfiguration();

            



            ConfigureAuth(app);
        }
    }
}
