using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Application.Interfaces;
using AuthService.Application.Services;
using AuthService.Domain.Repositories;
using AuthService.Infra.Data.Context;
using AuthService.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AuthService.Infra.IoC
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAuthService, Application.Services.AuthService>(); // JWT token generator
            services.AddScoped<ILoginAppService, LoginAppService>(); // Application service
            services.AddScoped<IUserRepository, UserRepository>(); // User repository

            services.AddAutoMapper(typeof(Application.Mappings.DomainToDTOMappingProfile));

            return services;
        }
    }
}