using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace newZealandWalksAPI.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        // Override OnActionExecuting for custom validations
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
