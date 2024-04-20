using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Microsoft.AspNetCore.Builder;


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
           


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(Optins =>
            {
                Optins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof (IGenericRepository<>),typeof (GenericRepository<>));

            //   builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

