using MentorApi.Model;
using MentorApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MentorApi.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                        

                    var errorDTO = new ErrorDTO(errors.ToList());
                    var response = new ResponseModel<ErrorDTO>
                    {
                        Data = errorDTO,
                        Status = 400
                    };

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
