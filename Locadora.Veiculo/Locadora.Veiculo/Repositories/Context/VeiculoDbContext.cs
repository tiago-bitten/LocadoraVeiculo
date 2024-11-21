using Microsoft.EntityFrameworkCore;
using Locadora.Veiculo.Models;

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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Manutencao>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Status)
                .HasConversion<string>();

            entity.Property(x => x.Id)
                .HasDefaultValueSql("'manutencao_' || abs(random() % 89999999 + 10000000)");
        });

        modelBuilder.Entity<Models.Veiculo>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Modelo)
                .HasConversion<string>();

            entity.Property(x => x.Tipo)
                .HasConversion<string>();

            entity.Property(x => x.Status)
                .HasConversion<string>();

            entity.Property(x => x.Id)
                .HasDefaultValueSql("'veiculo_' || abs(random() % 89999999 + 10000000)");
        });
    }

}