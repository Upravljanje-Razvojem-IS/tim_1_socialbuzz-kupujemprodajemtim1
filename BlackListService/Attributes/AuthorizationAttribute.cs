using BlackListService.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BlackListService.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _fakeToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authorizationHeader == null)
            {
                throw new BaseException("Unauthorized", 401);
            }

            string token = authorizationHeader.Substring("Bearer ".Length);

            if (token != _fakeToken)
            {
                throw new BaseException("Unauthorized", 401);
            }
        }
    }
}
