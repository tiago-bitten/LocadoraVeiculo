using Locadora.Veiculo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Veiculo.Repositories.Configs;

public class ManutencaoConfig : EntidadeBaseConfig<Models.Manutencao>
{
    public override void Configure(EntityTypeBuilder<Manutencao> builder)
    {
        builder.ToTable("Manutencao");
        
        builder.Property(x => x.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>();

        builder.Property(x => x.CodigoVeiculo)
            .HasColumnName("veiculo_id");
        
        builder.Property(x => x.DataInicio)
            .HasColumnName("data_inicio");
        
        builder.Property(x => x.DataFinal)
            .HasColumnName("data_final");
        
        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>();
        
        builder.Property(x => x.Id)
            .HasDefaultValueSql("'manutencao_' || abs(random() % 89999999 + 10000000)");
   
        builder.HasOne(x => x.Veiculo)
            .WithMany(x => x.Manutencoes)
            .HasForeignKey(x => x.CodigoVeiculo);
        
        base.Configure(builder);
    }
}