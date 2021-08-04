using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagement.Api.Helpers
{
    public class WebApiAuthorizeHandler : IAuthorizationFilter
    {
        private readonly ICollection<string> roles;

        public WebApiAuthorizeHandler(ICollection<string> roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else
            {
                var claims = user.Claims.ToList();

                if (this.roles.Any())
                {
                    var currentUserRole = claims?.FirstOrDefault(x => x.Type.EndsWith(@"/role", StringComparison.OrdinalIgnoreCase))?.Value;

                    if (!this.roles.Contains(currentUserRole))
                    {
                        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    }
                }
            }
        }
    }

    public class AllowAnonymousWithPolicyAttribute : TypeFilterAttribute, IAllowAnonymous
    {
        public AllowAnonymousWithPolicyAttribute() : base(typeof(WebApiAuthorizeHandler))
        {
            Arguments = new object[] { new string[0] };
        }

        public AllowAnonymousWithPolicyAttribute(string roles) : base(typeof(WebApiAuthorizeHandler))
        {
            Arguments = new object[] { roles.Split(", ") };
        }
    }
}
