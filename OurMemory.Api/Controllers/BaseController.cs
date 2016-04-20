using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

namespace OurMemory.Controllers
{
    public class BaseController : ApiController
    {
        protected ApplicationUserManager _userManager;
        public BaseController()
        {
           
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                var applicationUserManager = _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

                return applicationUserManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        [System.Web.Http.NonAction]
        public string GenerateAbsolutePath(string virtualPath)
        {
            return HttpContext.Current.Request.Url.Scheme +
                           "://"
                           + HttpContext.Current.Request.Url.Authority + virtualPath;
        }
    }
}