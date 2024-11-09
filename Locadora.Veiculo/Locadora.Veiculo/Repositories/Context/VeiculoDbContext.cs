using Microsoft.EntityFrameworkCore;
using Locadora.Veiculo.Models;

namespace Locadora.Veiculo.Repositories.Context;

public class VeiculoDbContext : DbContext
{
    public VeiculoDbContext(DbContextOptions<VeiculoDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Veiculo> Veiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Veiculo>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Modelo)
                .HasConversion<string>();

            entity.Property(x => x.Tipo)
                .HasConversion<string>();

            entity.Property(x => x.Status)
                .HasConversion<string>();
        });

        modelBuilder.Entity<Models.Manutencao>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Status)
                .HasConversion<string>();
        });
    }
}