using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital_Management.Api.Filters
{
    /// <summary>
    /// Action filter that validates ModelState before the action executes.
    /// Returns 400 Bad Request with validation errors if the model is invalid.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var result = new
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
