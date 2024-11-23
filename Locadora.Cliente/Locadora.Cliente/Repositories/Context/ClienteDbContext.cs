using Locadora.Cliente.Repositories.Configs;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Cliente.Repositories.Context;

public class ClienteDbContext : DbContext
{
    public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClienteConfig());
        
        base.OnModelCreating(modelBuilder);
    }
}