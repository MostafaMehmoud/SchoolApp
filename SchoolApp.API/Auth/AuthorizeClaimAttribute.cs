using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolApp.API.Auth
{
    public class AuthorizeClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimType;

        public AuthorizeClaimAttribute(string claimType)
        {
            _claimType = claimType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null || !jsonToken.Claims.Any(c => c.Type == _claimType && c.Value == "True"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
