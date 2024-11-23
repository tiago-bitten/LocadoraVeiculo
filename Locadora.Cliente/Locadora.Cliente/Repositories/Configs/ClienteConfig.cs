using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Cliente.Repositories.Configs;

public class ClienteConfig : EntidadeBaseConfig<Models.Cliente>
{
    public override void Configure(EntityTypeBuilder<Models.Cliente> builder)
    {
        builder.ToTable("clientes");

        builder.Property(x => x.Nome)
            .HasColumnName("nome");

        builder.Property(x => x.Cpf)
            .HasColumnName("cpf");
        
        builder.Property(x => x.Email)
            .HasColumnName("email");
        
        builder.Property(x => x.DataNascimento)
            .HasColumnName("data_nascimento")
            .IsRequired();

        builder.Property(x => x.Telefone)
            .HasColumnName("telefone");

        builder.Property(x => x.Endereco)
            .HasColumnName("endereco");
        
        builder.Property(x => x.Id)
            .HasDefaultValueSql("'cliente_' || abs(random() % 89999999 + 10000000)");

        base.Configure(builder);
    }
}