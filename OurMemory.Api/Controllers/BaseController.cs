

using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;

namespace OurMemory.Controllers
{
    public class BaseController : ApiController
    {
        protected ApplicationUserManager _userManager;
        public BaseController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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
    }
}