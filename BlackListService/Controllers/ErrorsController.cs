using BlackListService.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlackListService.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("errors")]
        public void Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception ex = context?.Error;
            HttpContext.Response.ContentType = "application/json";

            int statusCode = 500;
            string message = "Internal server error";

            if (ex is BaseException baseException)
            {
                statusCode = baseException.StatusCode;
                message = baseException.Message;
            }
            else if (ex != null)
            {
                statusCode = Response.StatusCode;
                message = ex.Message;
            }

            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.WriteAsync(message);
        }
    }
}
