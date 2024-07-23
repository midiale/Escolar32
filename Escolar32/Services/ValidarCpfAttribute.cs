using System.ComponentModel.DataAnnotations;

namespace Escolar32.Services
{
    
    public class ValidarCPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string cpf = value.ToString();

            // Remover caracteres não numéricos do CPF
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !IsNumeric(cpf))
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            // Verifica se os dígitos não são sequenciais
            bool isSequencial = Enumerable.Range(0, 9).All(i => cpf.Substring(i, 3).Select(c => c - '0').ToArray().SequenceEqual(new[] { i, i + 1, i + 2 }));
            if (isSequencial)
                return false;

            // Calculando os dígitos verificadores
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            
            if (digitoVerificador1 != int.Parse(cpf[9].ToString()) || digitoVerificador2 != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }

        private bool IsNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
      
}
