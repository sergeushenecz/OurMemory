using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OurMemory;
using OurMemory.Hubs;
using OurMemory.Service.Services;
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

            ConfigureAuth(app);
            //            app.MapSignalR();
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });

        }
    }
}
