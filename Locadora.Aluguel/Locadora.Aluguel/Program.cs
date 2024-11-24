using Hangfire;
using Hangfire.Storage.SQLite;
using Locadora.Aluguel.AplicServices;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services;
using Locadora.Aluguel.Services.Integracoes;
using Locadora.Aluguel.Services.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLite") 
                       ?? "Data Source=AIAIA.db";

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AluguelDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepAluguel, RepAluguel>();
builder.Services.AddScoped<IAplicAluguel, AplicAluguel>();
builder.Services.AddScoped<IServAluguel, ServAluguel>();
builder.Services.AddHttpClient<IClienteHelper, ClienteHelper>();
builder.Services.AddHttpClient<IVeiculoHelper, VeiculoHelper>();
builder.Services.AddScoped<IAluguelJob, AluguelJob>();
builder.Services.AddAutoMapper(typeof(AluguelProfile));

builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage()
);

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 5;
    options.Queues = ["alugueis", "default"];
});

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Locadora Aluguel API",
        Version = "v1",
        Description = "API para gerenciamento de alugueis na Locadora.",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Suporte",
            Email = "suporte@locadora.com",
            Url = new Uri("https://www.locadora.com")
        },
        License = new OpenApiLicense
        {
            Name = "Licença MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    options.CustomSchemaIds(type => type.FullName);
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora Aluguel API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHangfireDashboard();

JobsConfig.Criar();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
