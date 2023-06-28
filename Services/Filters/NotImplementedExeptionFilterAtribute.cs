using Domain.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Services.Filters
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { errorMessage = context.Exception.Message });
            }
            else if (context.Exception is ValidationException)
            {
                context.Result = new BadRequestObjectResult(new { errorMessage = context.Exception.Message });
            }
            else if (context.Exception is LoginException)
            {
                context.Result = new UnauthorizedResult();
            }
            else if (context.Exception is UnautorizeException)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
