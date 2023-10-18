using API.Extentions;
using API.Helpers;
using API.Middleware;
using Core.Entities.Identity;
using Infraestructure.Data;
using Infraestructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddSwaggerDocumentation();
            

            var connectionString = config.GetConnectionString("DefaultConnection");
            // Add database service
            builder.Services.AddDbContext<StoreContext>(opt => opt.UseSqlite(connectionString));
            // Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            builder.Services.AddApplicationService();
            builder.Services.AddIdentityService(builder.Configuration);

            // setup in this case that you don't know the type
            builder.Services.AddAutoMapper(typeof(MappingProfiles));




            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                builder =>
                {
                    builder.WithOrigins(
                                        "https://localhost:4200"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            // error handling needs this
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            // UseCors
            app.UseCors("AllowAngularOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            // swagger configuration
            app.UseSwaggerDocumention();

            app.UseStaticFiles();

            app.MapControllers();
            // Create a Database if it's not exists
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await identityContext.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                    await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An Error ocurred during Migration");
                }
            }

            app.Run();
        }
    }
}