namespace CourseManagement.Api.Authorization
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CustomAuthorizationAttribute : TypeFilterAttribute, IAllowAnonymous
    {
        /// <summary>
        /// Used when the attribute handles no specific Role
        /// </summary>
        public CustomAuthorizationAttribute() : base(typeof(CustomAuthorizeFilter))
        {
            this.Arguments = new object[] { new string[0] };
        }

        /// <summary>
        /// Used when the attribute handles one or more specific Roles. The roles are passed in the form of a string separated by ','
        /// </summary>
        /// <param name="roles"></param>
        public CustomAuthorizationAttribute(string roles) : base(typeof(CustomAuthorizeFilter))
        {
            this.Arguments = new object[] { roles.Split(", ") };
        }
    }
}
