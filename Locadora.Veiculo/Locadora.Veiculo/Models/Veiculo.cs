using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Helpers;

namespace Locadora.Veiculo.Models;

public class Veiculo : EntidadeBase
{
    public string Marca { get; set; }
    public EModeloVeiculo Modelo { get; set; }
    public ETipoVeiculo Tipo { get; set; }
    public DateTime DataFabricacao { get; set; }
    public string Placa { get; set; }
    public EStatusVeiculo Status { get; private set; } = EStatusVeiculo.Disponivel;
    public decimal ValorDiaria { get; set; }
    
    public int AnoFabricacao => DataFabricacao.Date.Year;
    
    //

    public List<Manutencao> Manutencoes { get; set; } = [];
    
    #region Regras
    
    #region ValidarDataFabricacao
    public void ValidarDataFabricao()
    {
        var dataLimite = new DateTime(1900, 1, 1);
        if (DataFabricacao < dataLimite)
            throw new VeiculoAppException(ETipoException.DataFabricacaoInvalida);
    }
    #endregion

    #region ValidarPlaca
    public void ValidarPlaca()
    {
        var placaInvalida = !PlacaHelper.ValidarPlaca(Placa);
        if (placaInvalida)
            throw new VeiculoAppException(ETipoException.PlacaInvalida);
    }
    #endregion
    
    #region ValidarCompatibilidadeTipoModelo
    public void ValidarCompatibilidadeTipoModelo()
    {
        var modelosPermitidosPorTipo = new Dictionary<ETipoVeiculo, List<EModeloVeiculo>>
        {
            { ETipoVeiculo.Carro,
                [
                    EModeloVeiculo.Sedan, EModeloVeiculo.Hatch, EModeloVeiculo.SUV, EModeloVeiculo.Coupe,
                    EModeloVeiculo.Conversivel, EModeloVeiculo.Perua, EModeloVeiculo.Pickup
                ]
            },
            { ETipoVeiculo.Moto,
                [
                    EModeloVeiculo.Esportiva, EModeloVeiculo.Cruiser, EModeloVeiculo.Scooter, EModeloVeiculo.Touring,
                    EModeloVeiculo.Trail
                ]
            },
            { ETipoVeiculo.Caminhao,
                [
                    EModeloVeiculo.CaminhaoPequeno, EModeloVeiculo.CaminhaoMedio, EModeloVeiculo.CaminhaoGrande,
                    EModeloVeiculo.Carreta
                ]
            },
            { ETipoVeiculo.Van, [EModeloVeiculo.VanPassageiros, EModeloVeiculo.VanCarga] },
            { ETipoVeiculo.SUV, [EModeloVeiculo.SUVCompacto, EModeloVeiculo.SUVGrande] },
            { ETipoVeiculo.Onibus,
                [EModeloVeiculo.MicroOnibus, EModeloVeiculo.OnibusUrbano, EModeloVeiculo.OnibusRodoviario]
            },
            { ETipoVeiculo.Outros, [EModeloVeiculo.Outro] }
        };

        if (!modelosPermitidosPorTipo.TryGetValue(Tipo, out var value) || !value.Contains(Modelo))
        {
            throw new VeiculoAppException(ETipoException.TipoModeloIncompativel);
        }
    }
    #endregion
    
    #region Status
    public void Disponibilizar() => Status = EStatusVeiculo.Disponivel;
    public void Alugar() => Status = EStatusVeiculo.Alugado;
    public void EmManutencao() => Status = EStatusVeiculo.EmManutencao;
    public void Reservar() => Status = EStatusVeiculo.Reservado;
    public void Vender() => Status = EStatusVeiculo.Vendido;
    #endregion
    #endregion
}

#region Enums
public enum EStatusVeiculo
{
    Disponivel = 1,
    Alugado = 2,
    EmManutencao = 3,
    Reservado = 4,
    Vendido = 5
}

public enum ETipoVeiculo
{
    Carro = 1,
    Moto = 2,
    Caminhao = 3,
    Van = 4,
    SUV = 5,
    Onibus = 6,
    Outros = 99
}

public enum EModeloVeiculo
{
    // Modelos de Carros
    Sedan = 1,
    Hatch = 2,
    SUV = 3,
    Coupe = 4,
    Conversivel = 5,
    Perua = 6,
    Pickup = 7,
    
    // Modelos de Motos
    Esportiva = 20,
    Cruiser = 21,
    Scooter = 22,
    Touring = 23,
    Trail = 24,
    
    // Modelos de Caminhões
    CaminhaoPequeno = 30,
    CaminhaoMedio = 31,
    CaminhaoGrande = 32,
    Carreta = 33,
    
    // Modelos de Vans e SUVs
    VanPassageiros = 40,
    VanCarga = 41,
    SUVCompacto = 42,
    SUVGrande = 43,
    
    // Modelos de Ônibus
    MicroOnibus = 50,
    OnibusUrbano = 51,
    OnibusRodoviario = 52,
    
    // Outros
    Outro = 99
}
#endregion