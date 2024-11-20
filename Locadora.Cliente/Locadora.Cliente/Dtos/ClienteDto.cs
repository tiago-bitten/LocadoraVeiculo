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

public record AdicionarClienteDto(
    string Nome, 
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);

public record ResultadoClienteDto(
    string Id,
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);
    
public record ClienteValidoDto(bool Valido, string Mensagem);