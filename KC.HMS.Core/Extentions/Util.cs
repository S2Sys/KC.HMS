using System.Security.Claims;
using System.Security.Principal;

namespace KC.HMS.Core.Extentions
{
    public static class Util
    {
        public static string GetCliam(this IPrincipal user, string name)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(name);
            return claim == null ? null : claim.Value;
        }

        public static string GetToken(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("api-token");
            return claim == null ? null : claim.Value;
        }
    }
}
