using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OurMemory;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

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
            ConfigureAuth(app);
        }
    }
}
