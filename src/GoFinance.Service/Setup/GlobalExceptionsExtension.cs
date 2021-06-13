using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace GoFinance.Service.Setup
{
    public static class GlobalExceptionsExtension
    {
        public static void UseGlobalExceptions(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async ctx =>
                {
                    var errorApp = ctx.Features.Get<IExceptionHandlerFeature>();
                    var ex = errorApp.Error;

                    ctx.Response.StatusCode = (int)ex.GetStatusCode();
                    ctx.Response.ContentType = "application/json";

                    var success = false;
                    var message = ex.Message;
                    var messageType = ex.GetMessageType();

                    var strJson = $@"{{ ""sucess"": {success}, ""message"": ""{message}"", ""message_type"": ""{messageType}"" }}";
                    await ctx.Response.WriteAsync(strJson);
                });
            });
        }

        private static HttpStatusCode GetStatusCode(this Exception exception)
        {
            //if (exception is IException exceptionResult)
            //    return exceptionResult.GetStatusCode();

            return HttpStatusCode.InternalServerError;
        }

        private static string GetMessageType(this Exception exception)
        {
            //if (exception is IException exceptionResult)
            //    return exceptionResult.GetMessageType();

            return "error";
        }
    }
}
