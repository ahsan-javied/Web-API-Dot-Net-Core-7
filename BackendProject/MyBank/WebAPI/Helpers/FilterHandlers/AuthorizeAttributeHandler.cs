using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.User;
using Models.Common;

namespace WebAPI.Helpers.FilterHandlers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context?.HttpContext?.Items["User"] as AuthenticatedUserDTO;
            if (user == null)
            {
                context.Result = new JsonResult(
                    new APIResponse(
                        false, StatusCodes.Status401Unauthorized, "Unauthorized"
                        , null
                        )
                    );
            }
        }
    }
}
