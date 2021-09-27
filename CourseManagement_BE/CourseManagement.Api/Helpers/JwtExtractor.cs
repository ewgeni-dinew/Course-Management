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
    }
}
