using Locadora.Cliente.Enterprise;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLite") 
                       ?? "Data Source=Cliente-Error.db";

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.ConfigureDatabase(connectionString);
builder.Services.ConfigureServices();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Locadora API",
        Version = "v1",
        Description = "API de gerenciamento de Clientes.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe Locadora",
            Email = "suporte@locadora.com",
            Url = new Uri("https://www.locadora.com"),
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Licença MIT",
            Url = new Uri("https://opensource.org/licenses/MIT"),
        }
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();