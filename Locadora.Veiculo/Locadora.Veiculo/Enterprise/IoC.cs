using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Repositories.Context;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.Enterprise
{
    public static class IoC
    {
        #region Db
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<VeiculoDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepVeiculo, RepVeiculo>();
            
            return services;
        }
        #endregion

        #region Services
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAplicVeiculo, AplicVeiculo>();
            services.AddScoped<IServVeiculo, ServVeiculo>();
            
            return services;
        }
        #endregion

        #region AutoMapper

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(VeiculoProfile));
            
            return services;
        }
        #endregion
    }
}