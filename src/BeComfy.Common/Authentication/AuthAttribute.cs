using Microsoft.AspNetCore.Authorization;

namespace BeComfy.Common.Authentication
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string scheme, string policy = "") 
            : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}