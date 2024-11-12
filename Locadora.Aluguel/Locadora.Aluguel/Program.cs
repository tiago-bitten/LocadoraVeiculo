using Locadora.Aluguel.AplicServices;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services;
using Locadora.Aluguel.Services.Integracoes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLite") 
                       ?? "Data Source=AIAIA.db";

builder.Services.AddDbContext<AluguelDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepAluguel, RepAluguel>();

builder.Services.AddScoped<IAplicAluguel, AplicAluguel>();
builder.Services.AddScoped<IServAluguel, ServAluguel>();
builder.Services.AddHttpClient<IClienteHelper, ClienteHelper>();

builder.Services.AddAutoMapper(typeof(AluguelProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();