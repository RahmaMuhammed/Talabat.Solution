using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class SwaggerServicesExtention
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();


            return Services;
        }

        public static void UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
        }


    }
}
