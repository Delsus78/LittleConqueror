using LittleConqueror.AppService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LittleConqueror.Exceptions;

public class AppExceptionFiltersAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not AppException exception) return;
        
        var errorResponse = new 
        {
            error = exception.Message
        };
            
        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = exception.ErrorCode
        };
    }
}