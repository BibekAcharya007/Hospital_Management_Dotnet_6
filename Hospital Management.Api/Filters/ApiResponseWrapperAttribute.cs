using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital_Management.Api.Filters
{
    public class ApiResponseWrapperAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
            {
                var wrapped = new
                {
                    Success = true,
                    Message = "Request completed successfully",
                    Data = objectResult.Value
                };

                context.Result = new ObjectResult(wrapped)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }
}
