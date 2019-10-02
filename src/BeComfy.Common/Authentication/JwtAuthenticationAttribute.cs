using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BeComfy.Common.Authentication
{
    public class JwtAuthenticationAttribute : AuthAttribute
    {
        public JwtAuthenticationAttribute(string policy = "") 
            : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}