using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Veiculo.Repositories.Configs;

public class VeiculoConfig : EntidadeBaseConfig<Models.Veiculo>
{
    public override void Configure(EntityTypeBuilder<Models.Veiculo> builder)
    {
        builder.ToTable("Veiculo");
        
        builder.Property(x => x.Modelo)
            .HasColumnName("modelo")
            .HasConversion<string>();
        
        builder.Property(x => x.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>();
        
        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>();
        
        builder.Property(x => x.Id)
            .HasDefaultValueSql("'veiculo_' || abs(random() % 89999999 + 10000000)");
        
        builder.HasMany(x => x.Manutencoes)
            .WithOne(x => x.Veiculo)
            .HasForeignKey(x => x.CodigoVeiculo);
        
        base.Configure(builder);
    }
}