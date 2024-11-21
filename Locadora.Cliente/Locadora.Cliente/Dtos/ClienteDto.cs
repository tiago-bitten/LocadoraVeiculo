using AutoMapper;

namespace Locadora.Cliente.Dtos;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<AdicionarClienteDto, Models.Cliente>();
        CreateMap<Models.Cliente, ResultadoClienteDto>();
    }
}

#region AdicionarClienteDto
public record AdicionarClienteDto(
    string Nome, 
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);
#endregion

#region ResultadoClienteDto
public record ResultadoClienteDto(
    string Id,
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);
#endregion

#region AtualizarClienteDto
public record AtualizarClienteDto(
    string CodigoCliente,
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);
#endregion
    
#region ClienteValidoDto
public record ClienteValidoDto(bool Valido, string Mensagem);
#endregion
