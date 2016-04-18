using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace OurMemory.Common
{
    public static class ConfigurationSettingsModule
    {
        public static string GetItem(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[key];
        }
    }

}
