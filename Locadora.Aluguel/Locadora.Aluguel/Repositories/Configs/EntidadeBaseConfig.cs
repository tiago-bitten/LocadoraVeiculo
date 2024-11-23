using Locadora.Aluguel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Aluguel.Repositories.Configs;

public class EntidadeBaseConfig<T> : IdentificadorConfig<T> where T : EntidadeBase
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.DataCriacao)
            .HasColumnName("data_criacao");
        
        builder.Property(x => x.DataAlteracao)
            .HasColumnName("data_alteracao");
        
        builder.Property(x => x.Inativo)
            .HasColumnName("inativo");
        
        base.Configure(builder);
    }
}