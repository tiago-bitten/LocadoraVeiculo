namespace Locadora.Aluguel.Dtos;

public class AdicionarAluguelDto
{
    public string CodigoCliente { get; set; }
    public string CodigoVeiculo { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
}