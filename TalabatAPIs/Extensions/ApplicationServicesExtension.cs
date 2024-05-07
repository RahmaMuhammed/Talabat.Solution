using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Generic_Repository;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //   Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            Services.AddAutoMapper(typeof(MappingProfile));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();

                    var Response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(Response);

                };
            });

            return Services;
        }
    }
}
