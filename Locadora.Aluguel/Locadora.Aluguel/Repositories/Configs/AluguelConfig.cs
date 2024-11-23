using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Aluguel.Repositories.Configs;

public class AluguelConfig : EntidadeBaseConfig<Models.Aluguel>
{
    public override void Configure(EntityTypeBuilder<Models.Aluguel> builder)
    {
        builder.Property(x => x.DataInicio)
            .HasColumnName("data_inicio");
        
        builder.Property(x => x.DataFinal)
            .HasColumnName("data_final");
        
        builder.Property(x => x.ValorTotal)
            .HasColumnName("valor_total");
        
        builder.Property(x => x.CodigoCliente)
            .HasColumnName("cliente_id");
        
        builder.Property(x => x.CodigoVeiculo)
            .HasColumnName("veiculo_id");
        
        builder.Property(x => x.Status)
            .HasColumnName("status");
        
        builder.Property(x => x.Id)
            .HasDefaultValueSql("'aluguel_' || abs(random() % 89999999 + 10000000)");

        base.Configure(builder);
    }
}