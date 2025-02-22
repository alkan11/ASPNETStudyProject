using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace WebApiProject.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerProvider loggerProvider)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {

                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _=>StatusCodes.Status500InternalServerError
                        };

                        loggerProvider.CreateLogger($"Something wnet wrong:{contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails() 
                        {
                            StatusCode=context.Response.StatusCode,
                            Message= contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
