using Locadora.Cliente.AplicServices;
using Locadora.Cliente.Dtos;
using Locadora.Cliente.Repositories;
using Locadora.Cliente.Repositories.Context;
using Locadora.Cliente.Repositories.Infra;
using Locadora.Cliente.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Cliente.Enterprise
{
    public static class IoC
    {
        #region Db
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ClienteDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepCliente, RepCliente>();
            
            return services;
        }
        #endregion

        #region Services
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServCliente, ServCliente>();
            services.AddScoped<IAplicCliente, AplicCliente>();

            return services;
        }
        #endregion

        #region AutoMapper

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ClienteProfile));
           
            return services;
        }
        #endregion

        #region AddCors
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()                              
                    .AllowAnyMethod()    
                          .AllowAnyHeader();   
                });
            });

            return services;
        }
        #endregion
    }
}