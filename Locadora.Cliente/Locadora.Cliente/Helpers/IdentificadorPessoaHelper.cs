using System;
using System.Text.RegularExpressions;

namespace Locadora.Cliente.Helpers
{
    public static partial class IdentificadorPessoaHelper
    {
        #region ValidarCpf
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = MeuRegex().Replace(cpf, "");

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{digito1}{digito2}");
        }
        #endregion

        #region ValidarEmail
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, padraoEmail, RegexOptions.IgnoreCase);
        }
        #endregion
        
        #region ValidarTelefone
        public static bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            if (telefone == "9999999999")
                return false;

            telefone = MeuRegex().Replace(telefone, "");

            return telefone.Length is 10 or 11;
        }
        #endregion

        //
        
        #region Regex
        [GeneratedRegex(@"[^\d]")]
        private static partial Regex MeuRegex();
        #endregion
    }
}
