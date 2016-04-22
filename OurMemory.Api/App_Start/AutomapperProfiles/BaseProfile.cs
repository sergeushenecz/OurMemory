using System.Web;

namespace OurMemory.AutomapperProfiles
{
    public class BaseProfile
    {
        public BaseProfile()
        {
        }

        private static string GetDomain
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority; }
        }
    }
}