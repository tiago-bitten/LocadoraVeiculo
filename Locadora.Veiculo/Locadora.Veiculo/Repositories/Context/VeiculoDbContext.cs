using Microsoft.EntityFrameworkCore;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Configs;

namespace Locadora.Veiculo.Repositories.Context;

public class VeiculoDbContext : DbContext
{
    public VeiculoDbContext(DbContextOptions<VeiculoDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Veiculo> Veiculos { get; set; }
    public DbSet<Manutencao> Manutencoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VeiculoConfig());
        modelBuilder.ApplyConfiguration(new ManutencaoConfig());
        
        base.OnModelCreating(modelBuilder);
    }

}