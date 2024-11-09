using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Repositories.Context;

public class AluguelDbContext : DbContext
{
    public AluguelDbContext(DbContextOptions<AluguelDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Aluguel> Aluguels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Aluguel>(entity => entity.HasKey(x => x.Id));
    }
}