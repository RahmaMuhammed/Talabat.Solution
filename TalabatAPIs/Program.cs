using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Middlewares;
using Talabat.APIs.Extentions;
using Talabat.APIs.Extensions;


namespace Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(Optins =>
            {
                Optins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //  ApplicationServicesExtension.AddAplicationServices(builder.Services);
            builder.Services.AddAplicationServices();

            var app = builder.Build();
            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var _dbContext = Services.GetRequiredService<StoreContext>();
            //Ask CLR To Create Object From DbContext Explicitly
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync(); //Update DataBase
                await StoreContextSeed.SeedAsync(_dbContext); //DataSeedin
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }

            app.UseMiddleware<ExeptionMiddleware>();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}

