using Hangfire;
using Hangfire.Storage.SQLite;
using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Repositories.Context;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;
using Locadora.Veiculo.Services.Jobs;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.Enterprise;

public static class IoC
{
    #region Db
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<VeiculoDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepVeiculo, RepVeiculo>();
        services.AddScoped<IRepManutencao, RepManutencao>();
            
        return services;
    }
    #endregion

    #region Services
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAplicVeiculo, AplicVeiculo>();
        services.AddScoped<IServVeiculo, ServVeiculo>();

        services.AddScoped<IAplicManutencao, AplicManutencao>();
        services.AddScoped<IServManutencao, ServManutencao>();
        
        services.AddScoped<IManutencaoJob, ManutencaoJob>();
            
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
        
    #region Hangfire
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services)
    {
        services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage()
        );

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = 5;
            options.Queues = ["default", "manutencoes"];
        });
            
        return services;
    }
    #endregion
}