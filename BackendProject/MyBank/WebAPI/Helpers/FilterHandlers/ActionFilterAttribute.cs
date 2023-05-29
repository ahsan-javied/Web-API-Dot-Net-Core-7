using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace WebAPI.Helpers.FilterHandlers
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public ValidateModelStateAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(
                    new APIResponse(
                        false, StatusCodes.Status400BadRequest, "BadRequest"
                        , context.ModelState
                        )
                    );
            }
        }
    }
}
