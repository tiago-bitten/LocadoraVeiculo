namespace Locadora.Veiculo.Models;

public abstract class EntidadeBase : Identificador
{
    public DateTime DataCriacao { get; private set; } = DateTime.Now;
    public DateTime DataAlteracao { get; private set; } = DateTime.Now;
    public bool Inativo { get; private set; } = false;

    #region Regras
    public void Atualizar() => DataAlteracao = DateTime.Now;
    public void Inativar() => Inativo = true;
    #endregion
}