using Locadora.Veiculo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Veiculo.Repositories.Configs;

public class IdentificadorConfig<T> : IEntityTypeConfiguration<T> where T : Identificador
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");
    }
}