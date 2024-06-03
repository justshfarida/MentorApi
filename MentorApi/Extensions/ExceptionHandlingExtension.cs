using MentorApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MentorApi.Extensions
{
    public static class ExceptionHandlingExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    await Console.Out.WriteLineAsync("Ise dusdu exhandler...");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server Error {contextFeature.Error}",
                        }));
                    }
                });
            });
        }
    }
}
