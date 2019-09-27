using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class ValidationCpf: IValidationBusiness
    {
        private static ValidationCpf instance = null;

        public static ValidationCpf Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidationCpf();
                }
                return instance;
            }
        }

        private ValidationCpf()
        {
        }

        /// <summary>
        /// Valida um cpf.
        /// </summary>
        /// <param name="value">String que contém um valor no formato 999.999.999-99 ou 99999999999.</param>
        /// <returns>Retorna true se o parâmetro value tiver um cpf válido.</returns>
        public bool IsValid(string value)
        {
            string cpf = value.Replace(",",".");

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public bool CpfIsNull(string value)
        {
            string valueMask = value.Replace(",", ".");

            return string.IsNullOrEmpty(valueMask) || valueMask == "   ,   ,   -";
        }
    }
}
