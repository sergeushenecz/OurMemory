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
