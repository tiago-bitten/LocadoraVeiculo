namespace Locadora.Cliente.Models;

public class EntidadeBase : Identificador
{
    public DateTime DataCriacao { get; } = DateTime.Now;
    public DateTime DataAlteracao { get; private set; } = DateTime.Now;
    public bool Inativo { get; private set; } = false;
    
    #region Regras
    public void Atualizar() => DataAlteracao = DateTime.Now;
    public void Inativar() => Inativo = true;
    #endregion
}