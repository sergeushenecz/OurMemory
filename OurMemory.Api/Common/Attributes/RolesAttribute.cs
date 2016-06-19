using System;
using System.Web.Http;

namespace OurMemory.Common.Attributes
{
    public class RolesAttribute: AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}