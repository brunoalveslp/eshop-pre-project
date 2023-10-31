using API.Errors;
using Core.Interfaces;
using Infraestructure.Data;
using Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // Add repository service
            // services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            // services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            // just like configure method inside startup class
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actioncontext =>
                {
                    var errors = actioncontext.ModelState
                                                .Where(e => e.Value.Errors.Count > 0)
                                                .SelectMany(e => e.Value.Errors)
                                                .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
