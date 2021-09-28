namespace CourseManagement.Api.Helpers
{
    using System.Linq;
    using System.Security.Claims;

    public static class JwtExtractor
    {
        public static int GetUserId(ClaimsIdentity identity)
        {
            return int.Parse(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static string GetUsername(ClaimsIdentity identity)
        {
            return identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }

    }
}
