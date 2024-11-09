using Locadora.Cliente.Enterprise;
using Locadora.Cliente.Helpers;

namespace Locadora.Cliente.Models;

public sealed class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }

    #region Regras
    public void ValidarCpf()
    {
        var cpfInvalido = !IdentificadorPessoaHelper.ValidarCpf(Cpf);
        if (cpfInvalido)
            throw new ClienteAppException(ETipoException.CpfInvalido);
    }

    public void ValidarEmail()
    {
        var emailInvalido = !IdentificadorPessoaHelper.ValidarEmail(Email);
        if (emailInvalido)
            throw new ClienteAppException(ETipoException.EmailInvalido);
    }

    public void ValidarDataNascimento()
    {
        var dataLimite = new DateTime(1900, 1, 1);
        if (DataNascimento < dataLimite)
            throw new ClienteAppException(ETipoException.DataNascimentoInvalida);
    }

    public void ValidarTelefone()
    {
        var telefoneInvalido = !IdentificadorPessoaHelper.ValidarTelefone(Telefone);
        if (telefoneInvalido)
            throw new ClienteAppException(ETipoException.TelefoneInvalido);
    }
    #endregion
}