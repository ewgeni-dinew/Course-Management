namespace CourseManagement.Api.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly ICollection<string> roles;

        public CustomAuthorizeFilter(ICollection<string> roles)
        {
            this.roles = roles;
        }

        /// <summary>
        /// Handles the specific authorization function.
        /// Uses the already implemented IsAuthenticated property.
        /// Returns HttpStatusCodes 401, 403 or 500 depending on the specific case.
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null)
            {
                //return the 500 error code when no used can be found from the request
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            else
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    //return 401 error code when the user is not authenticated
                    //this means the user JWT is *NOT VALID* or *EXPIRED*; the rest of the logic is performed by the FE;
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    //get all JWT claims
                    var claims = user.Claims.ToList();

                    if (this.roles.Any())
                    {
                        //get the specific claim for 'Role'
                        var currentUserRole = claims?.FirstOrDefault(x => x.Type.EndsWith(@"/role", StringComparison.OrdinalIgnoreCase))?.Value;

                        if (!this.roles.Contains(currentUserRole))
                        {
                            //user is authenticated successfully
                            //JWT is *VALID*
                            //user does not have permission; user role is not in the list of allowed roles;
                            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                        }
                    }
                }
            }
        }
    }
}
