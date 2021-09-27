using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Helpers
{
    public class JwtExtractor
    {
        public int GetUserId()
        {
            /*HttpContext context = HttpContext.Current;

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            return int.Parse(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);*/

            return 1;
        }
    }
}
